using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Interfaces.Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<bool> DoesExist(int Id , CancellationToken cancellationToken);

}
