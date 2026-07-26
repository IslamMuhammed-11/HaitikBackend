using System.Threading;
using System.Threading.Tasks;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Interfaces;

public interface IBulkUploadRejectedRowRepository : IGenericRepository<BulkUploadRejectedRow>
{
}
