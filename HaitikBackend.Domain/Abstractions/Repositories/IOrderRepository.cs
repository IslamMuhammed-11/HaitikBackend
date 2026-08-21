using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Abstractions.Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<bool> DoesExist(int Id, CancellationToken cancellationToken = default);

    Task<Order?> GetOrderAndReturnRequestAsync(int Id, CancellationToken cancellationToken = default);

    Task<Order?> GetByIdWithAssignmentsAsync(int Id, CancellationToken ct = default);

    Task<Order?> GetOrderAndAgencyByIdAsync(int Id, CancellationToken cancellationToken = default);
}
