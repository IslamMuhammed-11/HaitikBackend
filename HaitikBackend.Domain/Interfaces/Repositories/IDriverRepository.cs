using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Interfaces.Repositories;

public interface IDriverRepository : IGenericRepository<Driver>
{
    Task<bool> DoesExist(int userId, CancellationToken ct);

    Task<Driver> GetDriverWithLocationById(int Id, CancellationToken ct);
}
