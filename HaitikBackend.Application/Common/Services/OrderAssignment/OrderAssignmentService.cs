using HaitikBackend.Application.Common.Interfaces.OrderAssignment;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Application.Common.Services.OrderAssignment;

public class OrderAssignmentService : IOrderAssignmentService
{
    public async Task<Result> AcceptOrderAssignment(Order order, OrderDriverAssignment assignment, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = order.AssignDriver(assignment.DriverId);

            if (!result.IsSuccess)
                return result;

            assignment.MarkAsAccepted();

            return Result.Success();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result.Failed(OrderErrors.ConcurrecyConflict);
        }


    }
}
