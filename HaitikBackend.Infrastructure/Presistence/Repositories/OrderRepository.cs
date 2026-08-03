using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(HaitikDbContext context) : base(context)
    {
    }

    public async Task<bool> DoesExist(int Id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders.AnyAsync(o => o.Id == Id, cancellationToken);
    }

    public async Task<Order?> GetOrderAndReturnRequestAsync(int Id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Return)
            .FirstOrDefaultAsync(o => o.Id == Id, cancellationToken);
    }

    Task<Order?> GetByIdWithAssignmentsAsync(int Id, CancellationToken ct = default)
    {
        return _dbSet.Include(o => o.OrderDriverAssignments)
            .FirstOrDefaultAsync(o => o.Id == Id, ct);
    }

    public async Task<Order?> GetOrderAndAgencyByIdAsync(int Id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Agency)
            .FirstOrDefaultAsync(o => o.Id == Id, cancellationToken);
    }
}
