using HaitikBackend.Domain.Abstractions.Repositories;
using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class OrderDriverAssignmentRepository : GenericRepository<OrderDriverAssignment>, IOrderDriverAssignmentRepository
{
    public OrderDriverAssignmentRepository(HaitikDbContext context) : base(context)
    {
    }

    public async Task<OrderDriverAssignment?> GetByIdAsync(int orderId, int driverId, CancellationToken cancellationToken = default)
    {
        return await _context.OrderDriverAssignments
            .FirstOrDefaultAsync(oda => oda.OrderId == orderId && oda.DriverId == driverId, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<OrderDriverAssignment> orderDriverAssignments, CancellationToken cancellationToken = default)
    {
        await _context.OrderDriverAssignments.AddRangeAsync(orderDriverAssignments, cancellationToken);
    }
}
