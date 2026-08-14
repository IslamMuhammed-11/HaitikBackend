using HaitikBackend.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace HaitikBackend.Domain.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> DoesExistByEmail(string email, CancellationToken ct);

    Task<User?> GetUserAndRoleByEmail(string email, CancellationToken ct);

    Task<bool> DoesExistByPhoneNumber(string phoneNumber, CancellationToken ct);
}
