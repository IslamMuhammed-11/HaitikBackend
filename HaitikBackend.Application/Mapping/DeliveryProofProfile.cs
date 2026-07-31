using AutoMapper;
using HaitikBackend.Application.Features.DeliveryProofs.Queries.Responses;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Mapping;

public class DeliveryProofProfile : Profile
{
    public DeliveryProofProfile()
    {
        CreateMap<DeliveryProof, DeliveryProofResponse>();
    }
}
