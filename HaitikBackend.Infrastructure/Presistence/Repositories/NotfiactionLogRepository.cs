using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Interfaces.Repositories;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class NotfiactionLogRepository : GenericRepository<NotfiactionLog>, INotfiactionLogRepository
{
    public NotfiactionLogRepository(HaitikDbContext context) : base(context)
    {
    }
}
