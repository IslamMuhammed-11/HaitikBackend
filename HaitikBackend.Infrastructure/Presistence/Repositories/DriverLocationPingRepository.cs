using HaitikBackend.Domain.Abstractions.Repositories;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class DriverLocationPingRepository : GenericRepository<DriverLocationPing>, IDriverLocationPingRepository
{
    public DriverLocationPingRepository(HaitikDbContext context) : base(context)
    {
    }
}
