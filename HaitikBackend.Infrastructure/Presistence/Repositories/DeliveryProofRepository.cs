using HaitikBackend.Domain.Abstractions.Repositories;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Infrastructure.Presistence.Repositories;

internal class DeliveryProofRepository : GenericRepository<DeliveryProof>, IDeliveryProofRepository
{
    public DeliveryProofRepository(HaitikDbContext context) : base(context)
    {
    }
}
