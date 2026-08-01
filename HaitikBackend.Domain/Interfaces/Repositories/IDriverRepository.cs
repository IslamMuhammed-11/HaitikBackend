using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.ValueObjects;

namespace HaitikBackend.Domain.Interfaces.Repositories;

public interface IDriverRepository : IGenericRepository<Driver>
{
    Task<bool> DoesExist(int userId, CancellationToken ct = default);

    Task<Driver> GetDriverWithLocationById(int Id, CancellationToken ct = default);

    Task<ICollection<Driver>> GetDriversNearPickupLocation(GeoLocation deliveryLocatiom, CancellationToken ct = default);
}
