using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class GovernmentAgencyRepository : GenericRepository<GovernmentAgency>, IGovernmentAgencyRepository
{
    public GovernmentAgencyRepository(HaitikDbContext context) : base(context)
    {
    }

    public async Task<bool> DoesExistAsync(int id, CancellationToken ct = default)
    {
        return await _context.GovernmentAgencies.AnyAsync(a => a.Id == id, ct);
    }

    public async Task<bool> DoesExistAsync(string name, CancellationToken ct = default)
    {
        return await _context.GovernmentAgencies.AnyAsync(a => a.Name == name, ct);
    }


    public async Task<GovernmentAgency?> GetByEmail(string email, CancellationToken ct = default)
    {
        return await _context.GovernmentAgencies.FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task<GovernmentAgency?> GetByOrderId(int orderId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
