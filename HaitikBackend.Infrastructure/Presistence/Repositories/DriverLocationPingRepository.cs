using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Interfaces.Repositories;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class DriverLocationPingRepository : GenericRepository<DriverLocationPing>, IDriverLocationPingRepository
{
    public DriverLocationPingRepository(HaitikDbContext context) : base(context)
    {
    }
}
