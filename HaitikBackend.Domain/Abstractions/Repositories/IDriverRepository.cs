using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Models.Driver;
using HaitikBackend.Domain.ValueObjects;

namespace HaitikBackend.Domain.Abstractions.Repositories;

public interface IDriverRepository : IGenericRepository<Driver>
{
    Task<bool> DoesExist(int userId, CancellationToken ct = default);

    Task<Driver?> GetDriverWithLocationById(int Id, CancellationToken ct = default);

    Task<ICollection<DriverWithActiveOrdersCount>> GetDriversNearPickupLocation(GeoLocation PickupLocatiom, double Radius, CancellationToken ct = default);
}
