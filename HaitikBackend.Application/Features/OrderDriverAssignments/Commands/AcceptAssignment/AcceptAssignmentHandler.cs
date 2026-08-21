
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.DomainServices.OrderAssignmentService;
using HaitikBackend.Domain.Errors;
using MediatR;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Commands.AcceptAssignment;

public class AcceptAssignmentHandler : IRequestHandler<AcceptAssignmentCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderAssignmentService _orderAssignmentService;

    public AcceptAssignmentHandler(IUnitOfWork unitOfWork, IOrderAssignmentService orderAssignmentService)
    {
        _unitOfWork = unitOfWork;
        _orderAssignmentService = orderAssignmentService;
    }

    public async Task<Result> Handle(AcceptAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _unitOfWork.OrderDriverAssignments.GetByIdAsync(request.OrderId, request.DriverId);

        if (assignment is null)
            return Result.Failed(OrderDriverAssignmentErrors.AssignmentNotFound(request.OrderId, request.DriverId));

        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            return Result.Failed(OrderErrors.OrderNotFound(request.OrderId));

        var acceptResult = _orderAssignmentService.AcceptOrderAssignment(order, assignment, cancellationToken);

        if (!acceptResult.IsSuccess)
            return acceptResult;

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();

    }

}
