using AutoMapper;
using HaitikBackend.Application.Features.DeliveryProofs.Queries.Responses;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Features.DeliveryProofs;

public class DeliveryProofProfile : Profile
{
    public DeliveryProofProfile()
    {
        CreateMap<DeliveryProof, DeliveryProofResponse>();
    }
}
