using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.DomainServices.OrderAssignmentService;

public interface IOrderAssignmentService
{
        Result AcceptOrderAssignment(
        Order order,
        OrderDriverAssignment assignment,
        CancellationToken cancellationToken = default);
}
