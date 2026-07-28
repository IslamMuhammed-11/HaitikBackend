using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Interfaces.Repositories;

public interface IGeoZoneRepository : IGenericRepository<GeoZone>
{
    Task<bool> DoesExist(int Id);
}
