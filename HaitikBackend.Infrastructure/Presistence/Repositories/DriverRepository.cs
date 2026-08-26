
using HaitikBackend.Domain.Abstractions.Repositories;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Models.Driver;
using HaitikBackend.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class DriverRepository : GenericRepository<Driver>, IDriverRepository
{
    public DriverRepository(HaitikDbContext context) : base(context)
    {
    }

    public async Task<bool> DoesExist(int userId, CancellationToken ct = default)
    {
        return await _dbSet.AnyAsync(d => d.UserId == userId, ct);
    }

    public async Task<Driver?> GetDriverWithLocationById(int Id, CancellationToken ct = default)
    {
        return await _dbSet
            .Include(d => d.DriverLocationPing)
            .Include(d => d.Orders)
            .FirstOrDefaultAsync(d => d.UserId == Id, ct);
    }

    public async Task<ICollection<DriverWithActiveOrdersCount>> GetDriversNearPickupLocation(GeoLocation PickupLocatiom, double Radius, CancellationToken ct = default)
    {
        return await _context.Drivers
            .AsNoTracking()
            .Where(d => d.DriverLocationPing != null && d.Status != Domain.Enums.enDriverStatus.Offline)
            .Include(d => d.User)
            .Select(d => new
            {
                driver = d,
                distance = d.DriverLocationPing!.Location.CurrentLocation.Distance(PickupLocatiom.CurrentLocation),
                activeOrdersCount = d.Orders.Count(o => o.Status != Domain.Enums.enOrderStatus.Delivered)
            })
            .Where(d => d.distance <= Radius)
            .OrderBy(d => d.distance)
            .ThenBy(d => d.activeOrdersCount)
            .Select(x => new DriverWithActiveOrdersCount(x.driver, x.activeOrdersCount))
            .ToListAsync(ct);
    }
}
