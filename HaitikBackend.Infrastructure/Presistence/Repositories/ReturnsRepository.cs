using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Interfaces.Repositories;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class ReturnsRepository : GenericRepository<Return>, IReturnsRepository
{
    public ReturnsRepository(HaitikDbContext context) : base(context)
    {
    }
}
