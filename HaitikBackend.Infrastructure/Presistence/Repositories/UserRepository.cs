using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Interfaces.Repositories;
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

    public async Task<bool> DoesExistByPhoneNumber(string phoneNumber, CancellationToken ct)
    {
        return await _context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber, ct);
    }
}
