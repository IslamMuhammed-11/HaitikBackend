using System.Threading;
using System.Threading.Tasks;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Interfaces.Repositories;

public interface IGovernmentEmployeeRepository : IGenericRepository<GovernmentEmployee>
{
    Task<bool> DoesExist(int userId, CancellationToken ct);
}
