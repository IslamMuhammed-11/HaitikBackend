using System.Threading;
using System.Threading.Tasks;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> DoesExistByEmail(string email, CancellationToken ct);

    Task<bool> DoesExistByPhoneNumber(string phoneNumber, CancellationToken ct);
}
