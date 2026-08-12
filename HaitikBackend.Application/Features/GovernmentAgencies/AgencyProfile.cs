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
            .ForMember(dest => dest.latitude, opt => opt
            .MapFrom(e => e.Location.CurrentLocation.Y))
            .ForMember(dest => dest.longitude, opt => opt
            .MapFrom(e => e.Location.CurrentLocation.X))
            .ReverseMap();
    }
}
