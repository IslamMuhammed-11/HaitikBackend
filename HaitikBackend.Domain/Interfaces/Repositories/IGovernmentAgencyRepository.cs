using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Interfaces.Repositories;

public interface IGovernmentAgencyRepository : IGenericRepository<GovernmentAgency>
{
    Task<bool> DoesExistAsync(int id, CancellationToken ct = default);

    Task<bool> DoesExistAsync(string name, CancellationToken ct = default);

}
