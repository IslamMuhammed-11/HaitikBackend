using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Abstractions.Repositories;

public interface IOrderDriverAssignmentRepository : IGenericRepository<OrderDriverAssignment>
{
    Task<OrderDriverAssignment?> GetByIdAsync(int orderId, int driverId, CancellationToken cancellationToken = default);

    Task AddRangeAsync(IEnumerable<OrderDriverAssignment> orderDriverAssignments, CancellationToken cancellationToken = default);
}
