using HaitikBackend.Domain.Abstractions.Repositories;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(HaitikDbContext context) : base(context)
    {
    }
}
