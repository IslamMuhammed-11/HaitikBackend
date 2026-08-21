using HaitikBackend.Domain.Abstractions.Repositories;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class NotfiactionLogRepository : GenericRepository<NotfiactionLog>, INotfiactionLogRepository
{
    public NotfiactionLogRepository(HaitikDbContext context) : base(context)
    {
    }
}
