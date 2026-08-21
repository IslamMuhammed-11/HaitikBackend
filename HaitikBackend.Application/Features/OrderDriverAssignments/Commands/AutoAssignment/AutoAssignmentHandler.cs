using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Common.Models;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.DomainEvents.OrderDriverAssignment;
using HaitikBackend.Domain.Models.Driver;
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
    private readonly IBackgroundJobs _backgroundJobs;
    private readonly INotificationService _notificationService;
    public AutoAssignmentHandler(IUnitOfWork unitOfWork, IPublisher publisher, IBackgroundJobs backgroundJobs,
                                   INotificationService notificationService, ILogger<AutoAssignmentHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
        _backgroundJobs = backgroundJobs;
        _notificationService = notificationService;
        _logger = logger;
    }

    public async Task Handle(AutoAssignmentCommand request, CancellationToken cancellationToken)
    {

        var order = await _unitOfWork.Orders.GetOrderAndAgencyByIdAsync(request.orderId, cancellationToken);

        var drivers = await _unitOfWork.Drivers.GetDriversNearPickupLocation(order!.Agency.Location, Meters, cancellationToken);

        if (drivers.Count == 0)
            return;



        var assignments = drivers.Select(e => order.RequestToAssignDriver(e.Driver.UserId)).ToList();


        await _unitOfWork.OrderDriverAssignments.AddRangeAsync(assignments, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);




        var sendOffers = drivers.Select(d => _notificationService.SendOrderOfferNotificationAsync
                          (OrderNotificationModel.Create(order.Id, d.Driver.UserId, d.Driver.User.Email,
                          order.DeliveryLocation.CurrentLocation), AcceptanceWindow, cancellationToken));

        await Task.WhenAll(sendOffers);

        var driverIds = drivers.Select(d => new DriverIdWithActiveOrdersCount(d.Driver.UserId, d.ActiveOrdersCount)).ToList();

        await _publisher.Publish(new OrderOfferedToDriversEvent(order.Id, driverIds, AcceptanceWindow), cancellationToken);

    }


}
