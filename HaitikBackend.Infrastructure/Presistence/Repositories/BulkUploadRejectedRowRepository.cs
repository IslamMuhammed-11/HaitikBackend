using HaitikBackend.Domain.Abstractions.Repositories;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class BulkUploadRejectedRowRepository : GenericRepository<BulkUploadRejectedRow>, IBulkUploadRejectedRowRepository
{
    public BulkUploadRejectedRowRepository(HaitikDbContext context) : base(context)
    {
    }
}
