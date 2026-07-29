using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Commands.AcceptAssignment;

public class AcceptAssignmentHandler : IRequestHandler<AcceptAssignmentCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public AcceptAssignmentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AcceptAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _unitOfWork.OrderDriverAssignments.GetByIdAsync(request.OrderId, request.DriverId);

        if (assignment is null)
            return Result.Failed(OrderDriverAssignmentErrors.AssignmentNotFound(request.OrderId, request.DriverId));

        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            return Result.Failed(OrderErrors.OrderNotFound(request.OrderId));

        return await Execute(order, assignment, cancellationToken);

    }



    private async Task<Result> Execute(Order order, HaitikBackend.Domain.Entities.OrderDriverAssignment assignment, CancellationToken cancellationToken)
    {
        try
        {
            var result = order.AssignDriver(assignment.DriverId);

            if (!result.IsSuccess)
                return result;

            assignment.MarkAsAccepted();

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result.Failed(OrderErrors.ConcurrecyConflict);
        }


    }
}
