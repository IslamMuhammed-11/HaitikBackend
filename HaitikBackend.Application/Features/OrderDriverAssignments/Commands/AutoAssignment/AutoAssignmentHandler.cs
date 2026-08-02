using HaitikBackend.Application.Common.Interfaces.BackgroundJobs;
using HaitikBackend.Domain.DomainEvents.OrderDriverAssignment;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Commands.AutoAssignment;

public class AutoAssignmentHandler : IRequestHandler<AutoAssignmentCommand>
{
    //for now
    private readonly TimeSpan AcceptanceWindow = TimeSpan.FromMinutes(3);
    private readonly double Meters = 15000;


    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;
    private readonly IBackgroundJobs _backgroundJobs;
    public AutoAssignmentHandler(IUnitOfWork unitOfWork, IPublisher publisher, IBackgroundJobs backgroundJobs)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
        _backgroundJobs = backgroundJobs;
    }

    public async Task Handle(AutoAssignmentCommand request, CancellationToken cancellationToken)
    {

        var order = await _unitOfWork.Orders.GetOrderAndAgencyByIdAsync(request.orderId, cancellationToken);

        var drivers = await _unitOfWork.Drivers.GetDriversNearPickupLocation(order!.Agency.Location, Meters, cancellationToken);

        //if (drivers.ToArray().Length == 0)
        //    _backgroundJobs.EnqueueAutoAssignment()



        var assignments = drivers.Select(e => order.RequestToAssignDriver(e.UserId)).ToList();


        await _unitOfWork.OrderDriverAssignments.AddRangeAsync(assignments, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new OrderOfferedToDriversEvent(order.Id, assignments.Select(e => e.DriverId).ToList(), AcceptanceWindow), cancellationToken);

    }
}
