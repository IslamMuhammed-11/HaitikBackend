using HaitikBackend.Domain.Abstractions.Repositories;
using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(HaitikDbContext context) : base(context)
    {
    }

    public async Task<bool> DoesExistByEmail(string email, CancellationToken ct)
    {
        return await _context.Users.AnyAsync(u => u.Email == email, ct);
    }

    public async Task<User?> GetUserAndRoleByEmail(string email, CancellationToken ct)
    {
        return await _context.Users.Include(e => e.Role).FirstOrDefaultAsync(e => e.Email == email);
    }



    public async Task<bool> DoesExistByPhoneNumber(string phoneNumber, CancellationToken ct)
    {
        return await _context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber, ct);
    }

    public async Task<User?> GetUserAndRefreshTokensByEmail(string email, CancellationToken ct)
    {
        return await _context.Users.Include(e => e.RefreshTokens).FirstOrDefaultAsync(e => e.Email == email);
    }
}
