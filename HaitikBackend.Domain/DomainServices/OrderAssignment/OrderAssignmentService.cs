using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.DomainServices.OrderAssignmentService;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.DomainServices.OrderAssignment;

public class OrderAssignmentService : IOrderAssignmentService
{
    public Result AcceptOrderAssignment(
           Order order,
           OrderDriverAssignment assignment,
           CancellationToken cancellationToken = default)
    {
        var result = order.AssignDriver(assignment.DriverId);

        if (!result.IsSuccess)
            return result;

        assignment.MarkAsAccepted();

        order.SetStatusAsReceivedPackage();

        return Result.Success();
    }
}
