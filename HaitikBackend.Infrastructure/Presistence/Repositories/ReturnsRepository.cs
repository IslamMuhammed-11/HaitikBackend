using HaitikBackend.Domain.Abstractions.Repositories;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class ReturnsRepository : GenericRepository<Return>, IReturnsRepository
{
    public ReturnsRepository(HaitikDbContext context) : base(context)
    {
    }
}
