using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Interfaces.Repositories;
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
            .FirstOrDefaultAsync(d => d.UserId == Id, ct);
    }

    public async Task<ICollection<Driver>> GetDriversNearPickupLocation(GeoLocation PickupLocatiom, double Radius, CancellationToken ct = default)
    {
        return await _context.Drivers
            .AsNoTracking()
            .Where(d => d.DriverLocationPing != null && d.Status != Domain.Enums.enDriverStatus.Offline)
            .Select(d => new
            {
                driver = d,
                distance = d.DriverLocationPing!.Location.CurrentLocation.Distance(PickupLocatiom.CurrentLocation)
            })
            .Where(d => d.distance <= Radius)
            .OrderBy(d => d.distance)
            .Select(d => d.driver)
            .ToListAsync(ct);
    }
}
