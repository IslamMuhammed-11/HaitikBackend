using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Interfaces.Repositories;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class BulkUploadBatchRepository : GenericRepository<BulkUploadBatch>, IBulkUploadBatchRepository
{
    public BulkUploadBatchRepository(HaitikDbContext context) : base(context)
    {
    }
}
