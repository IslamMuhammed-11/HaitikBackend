using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Interfaces.Repositories;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(HaitikDbContext context) : base(context)
    {
    }
}
