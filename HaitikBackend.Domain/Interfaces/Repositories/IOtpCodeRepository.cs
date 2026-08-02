using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Interfaces.Repositories;

public interface IOtpCodeRepository : IGenericRepository<OtpCode>
{
    Task<OtpCode?> GetByOrderIdAsync(int orderId , enOtpPurpose purpose , CancellationToken cancellationToken);
}
