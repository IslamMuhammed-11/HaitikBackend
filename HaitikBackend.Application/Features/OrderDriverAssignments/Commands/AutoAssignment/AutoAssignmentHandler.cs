using HaitikBackend.Application.Common.Interfaces;
using HaitikBackend.Application.Common.Interfaces.BackgroundJobs;
using HaitikBackend.Domain.DomainEvents.OrderDriverAssignment;
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



        var assignments = drivers.Select(e => order.RequestToAssignDriver(e.DriverId)).ToList();


        await _unitOfWork.OrderDriverAssignments.AddRangeAsync(assignments, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var sendOffers = drivers.Select(d => _notificationService.SendOrderOfferNotificationAsync(d.DriverId, request.orderId, AcceptanceWindow, cancellationToken));

        await Task.WhenAll(sendOffers);

        await _publisher.Publish(new OrderOfferedToDriversEvent(order.Id, drivers.ToList(), AcceptanceWindow), cancellationToken);

    }


}
