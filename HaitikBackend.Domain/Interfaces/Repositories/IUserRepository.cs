using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> DoesExistByEmail(string email, CancellationToken ct);

    Task<User?> GetUserAndRoleByEmail(string email, CancellationToken ct);

    Task<User?> GetUserAndRefreshTokensByEmail(string email, CancellationToken ct);

    Task<bool> DoesExistByPhoneNumber(string phoneNumber, CancellationToken ct);
}
