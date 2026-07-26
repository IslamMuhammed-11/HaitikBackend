using System.Threading;
using System.Threading.Tasks;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Interfaces;

public interface IDriverRepository : IGenericRepository<Driver>
{
    Task<bool> DoesExist(int userId, CancellationToken ct);
}
