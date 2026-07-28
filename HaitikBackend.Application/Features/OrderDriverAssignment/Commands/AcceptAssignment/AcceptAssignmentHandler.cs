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

        assignment.MarkAsAccepted();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
