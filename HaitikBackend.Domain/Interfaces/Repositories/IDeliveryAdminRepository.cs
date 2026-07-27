using System.Threading;
using System.Threading.Tasks;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Domain.Interfaces.Repositories;

public interface IDeliveryAdminRepository : IGenericRepository<DeliveryAdmin>
{
    Task<bool> DoesExist(int userId, CancellationToken ct);
}
