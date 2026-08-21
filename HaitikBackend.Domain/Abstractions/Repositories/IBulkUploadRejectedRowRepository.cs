using System.Threading;
using System.Threading.Tasks;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Abstractions.Repositories;

public interface IBulkUploadRejectedRowRepository : IGenericRepository<BulkUploadRejectedRow>
{
}
