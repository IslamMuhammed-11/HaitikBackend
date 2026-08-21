using HaitikBackend.Domain.Abstractions.Repositories;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class OrderStatusHistoryRepository : GenericRepository<OrderStatusHistory>, IOrderStatusHistoryRepository
{
    public OrderStatusHistoryRepository(HaitikDbContext context) : base(context)
    {
    }
}
