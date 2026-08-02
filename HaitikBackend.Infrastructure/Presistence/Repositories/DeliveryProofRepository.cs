using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Interfaces.Repositories;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class DeliveryProofRepository : GenericRepository<DeliveryProof>, IDeliveryProofRepository
{
    public DeliveryProofRepository(HaitikDbContext context) : base(context)
    {
    }
}
