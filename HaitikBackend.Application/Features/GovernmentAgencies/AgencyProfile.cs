using AutoMapper;
using HaitikBackend.Application.Features.GovernmentAgencies.Qureies.Responses;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Features.GovernmentAgencies;

public class AgencyProfile : Profile
{
    public AgencyProfile()
    {
        CreateMap<GovernmentAgency, AgencyDetails>()
            .ForMember(dest => dest.TotalOrders, opt => opt
            .MapFrom(e => e.Orders.Count))
            .ReverseMap();
    }
}
