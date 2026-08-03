using HaitikBackend.Application.Common.Interfaces.BackgroundJobs;
using HaitikBackend.Application.Common.Interfaces.Notification;
using HaitikBackend.Application.Common.Models.OfferNotificationModel;
using HaitikBackend.Domain.DomainEvents.OrderDriverAssignment;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Commands.AutoAssignment;

public class AutoAssignmentHandler : IRequestHandler<AutoAssignmentCommand>
{
    //for now
    private readonly TimeSpan AcceptanceWindow = TimeSpan.FromMinutes(3);
    private readonly double Meters = 15000;


    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;
    private readonly ILogger<AutoAssignmentHandler> _logger;
    private readonly INotificationPush _notificationPush;
    private readonly IBackgroundJobs _backgroundJobs;
    public AutoAssignmentHandler(IUnitOfWork unitOfWork, IPublisher publisher, IBackgroundJobs backgroundJobs,
                                    INotificationPush notificationPush, ILogger<AutoAssignmentHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
        _backgroundJobs = backgroundJobs;
        _notificationPush = notificationPush;
        _logger = logger;
    }

    public async Task Handle(AutoAssignmentCommand request, CancellationToken cancellationToken)
    {

        var order = await _unitOfWork.Orders.GetOrderAndAgencyByIdAsync(request.orderId, cancellationToken);

        var drivers = await _unitOfWork.Drivers.GetDriversNearPickupLocation(order!.Agency.Location, Meters, cancellationToken);

        //if (drivers.ToArray().Length == 0)
        //    _backgroundJobs.EnqueueAutoAssignment()



        var assignments = drivers.Select(e => order.RequestToAssignDriver(e.UserId)).ToList();


        await _unitOfWork.OrderDriverAssignments.AddRangeAsync(assignments, cancellationToken);

        var save = _unitOfWork.SaveChangesAsync(cancellationToken);

        var sendOffers = SendOffersToDrivers(drivers, order.Id, AcceptanceWindow, cancellationToken);

        await Task.WhenAll(save, sendOffers);

        await _publisher.Publish(new OrderOfferedToDriversEvent(order.Id, assignments.Select(e => e.DriverId).ToList(), AcceptanceWindow), cancellationToken);

    }

    internal async Task SendOffersToDrivers(ICollection<Driver> drivers, int orderId, TimeSpan acceptanceWindow, CancellationToken cancellationToken)
    {
        await Parallel.ForEachAsync(drivers,
           new ParallelOptions
           {
               MaxDegreeOfParallelism = 20
           },
           async (driver, cancellationToken) =>
           {

               try
               {
                   var model = new OfferNotificationModel(driver.UserId, orderId, acceptanceWindow);

                   await _notificationPush.SendOrderOfferNotification(model, cancellationToken);
               }
               catch (Exception ex)
               {

                   _logger.LogError(ex, "Error sending offer notification to driver {DriverId} for order {OrderId}", driver.UserId, orderId);
               }


           });
    }



}
