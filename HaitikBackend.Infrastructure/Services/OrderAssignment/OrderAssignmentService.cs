using HaitikBackend.Application.Common.Interfaces.OrderAssignment;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Infrastructure.Services.OrderAssignment;

public class OrderAssignmentService : IOrderAssignmentService
{
    public Task<Result> AcceptOrderAssignment(
        Order order,
        OrderDriverAssignment assignment,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();
}
