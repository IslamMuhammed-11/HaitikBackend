using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Interfaces.Repositories;

public interface IGovernmentAgencyRepository : IGenericRepository<GovernmentAgency>
{
    Task<bool> DoesExistAsync(int id, CancellationToken ct = default);

    Task<bool> DoesExistAsync(string name, CancellationToken ct = default);

    Task<GovernmentAgency?> GetByEmail(string email, CancellationToken ct = default);

    Task<GovernmentAgency?> GetByOrderId(int orderId, CancellationToken ct = default);

}
