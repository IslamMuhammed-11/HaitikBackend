using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Commands.AutoAssignment;

public class AutoAssignmentHandler : IRequestHandler<AutoAssignmentCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    public AutoAssignmentHandler(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task Handle(AutoAssignmentCommand request, CancellationToken cancellationToken)
    {



        var order = await _unitOfWork.Orders.GetOrderAndAgencyByIdAsync(request.orderId, cancellationToken);

        var drivers = await _unitOfWork.Drivers.GetDriversNearPickupLocation(order!.Agency!.Location, cancellationToken);

        //if (drivers.ToArray().Length == 0) 
        // Trigger Delyed background job to look again



        var assignments = drivers.Select(e => order.RequestToAssignDriver(e.UserId)).ToList();


        await _unitOfWork.OrderDriverAssignments.AddRangeAsync(assignments, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        

    }
}
