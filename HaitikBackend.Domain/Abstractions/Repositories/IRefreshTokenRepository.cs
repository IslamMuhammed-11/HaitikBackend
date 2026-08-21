using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Abstractions.Repositories;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    Task<RefreshToken?> GetUserActiveToken(int userId);

    Task<RefreshToken?> GetAgencyActiveToken(int agencyId);
}
