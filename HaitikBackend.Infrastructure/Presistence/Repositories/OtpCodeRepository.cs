using HaitikBackend.Domain.Abstractions.Repositories;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class OtpCodeRepository : GenericRepository<OtpCode>, IOtpCodeRepository
{
    public OtpCodeRepository(HaitikDbContext context) : base(context)
    {
    }

    public async Task<OtpCode?> GetByOrderIdAsync(int orderId, enOtpPurpose purpose, CancellationToken cancellationToken)
    {
        return await _context.OtpCodes.FirstOrDefaultAsync(o => o.OrderId == orderId && o.Purpose == purpose, cancellationToken);
    }
}
