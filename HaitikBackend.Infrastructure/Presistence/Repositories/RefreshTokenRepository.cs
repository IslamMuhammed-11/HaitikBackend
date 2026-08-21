using HaitikBackend.Domain.Abstractions.Repositories;
using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(HaitikDbContext context) : base(context)
    {


    }

    public async Task<RefreshToken?> GetUserActiveToken(int userId)
    {
        return await _context.RefreshTokens.Where(e => e.RevokedAt == null && e.UserId == userId).FirstOrDefaultAsync();
    }


    public async Task<RefreshToken?> GetAgencyActiveToken(int agencyId)
    {
        return await _context.RefreshTokens.Where(e => e.RevokedAt == null && e.AgencyId == agencyId).FirstOrDefaultAsync();
    }

}
