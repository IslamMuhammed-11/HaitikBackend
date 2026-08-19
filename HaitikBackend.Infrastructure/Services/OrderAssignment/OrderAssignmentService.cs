using HaitikBackend.Application.Common.Interfaces.OrderAssignment;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Infrastructure.Services.OrderAssignment;

public class OrderAssignmentService : IOrderAssignmentService
{
    public async Task<Result> AcceptOrderAssignment(
        Order order,
        OrderDriverAssignment assignment,
        CancellationToken cancellationToken = default)
    {
        var result = order.AssignDriver(assignment.DriverId);

        if (!result.IsSuccess)
            return result;

        assignment.MarkAsAccepted();

        return Result.Success();
    }
}
