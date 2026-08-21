using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Commands.RejectAssignment;

public class RejectAssignmentHandler : IRequestHandler<RejectAssignmentCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public RejectAssignmentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RejectAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _unitOfWork.OrderDriverAssignments.Query()
            .Where(a => a.OrderId == request.OrderId && a.DriverId == request.DriverId)
            .FirstOrDefaultAsync(cancellationToken);

        if (assignment is null)
            return Result.Failed(OrderDriverAssignmentErrors.AssignmentNotFound(request.OrderId, request.DriverId));

        assignment.MarkAsRejected();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
