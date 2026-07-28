using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Interfaces.Repositories;

public interface IGovernmentEmployeeRepository : IGenericRepository<GovernmentEmployee>
{
    Task<bool> DoesExistByUserIdAsync(int userId, CancellationToken ct = default);

    Task<bool> DoesExistByIdAsync(int id, CancellationToken ct = default);
}
