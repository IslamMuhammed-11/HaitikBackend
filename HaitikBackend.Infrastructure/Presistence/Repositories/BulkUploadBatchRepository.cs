using HaitikBackend.Domain.Abstractions.Repositories;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class BulkUploadBatchRepository : GenericRepository<BulkUploadBatch>, IBulkUploadBatchRepository
{
    public BulkUploadBatchRepository(HaitikDbContext context) : base(context)
    {
    }
}
