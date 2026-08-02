using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Interfaces.Repositories;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class BulkUploadRejectedRowRepository : GenericRepository<BulkUploadRejectedRow>, IBulkUploadRejectedRowRepository
{
    public BulkUploadRejectedRowRepository(HaitikDbContext context) : base(context)
    {
    }
}
