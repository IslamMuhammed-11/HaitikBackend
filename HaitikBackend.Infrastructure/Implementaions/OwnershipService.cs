using HaitikBackend.Application.Abstractions;
using HaitikBackend.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Infrastructure.Implementaions;

public sealed class OwnershipService : IOwnershipService
{
    private readonly HaitikDbContext _dbContext;

    public OwnershipService(HaitikDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> CanAccessDriverAsync(int driverId, int orderId) =>
        _dbContext.Orders
            .AsNoTracking()
            .AnyAsync(order => order.Id == orderId && order.AssignedDriver == driverId);

    public Task<bool> CanAccessAgencyAsync(int agencyId, int orderId) =>
        _dbContext.Orders
            .AsNoTracking()
            .AnyAsync(order => order.Id == orderId && order.AgencyId == agencyId);
}
