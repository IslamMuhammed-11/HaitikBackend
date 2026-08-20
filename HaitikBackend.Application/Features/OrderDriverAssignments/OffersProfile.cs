using AutoMapper;
using HaitikBackend.Application.Features.OrderDriverAssignment.Queries.Responses;

namespace HaitikBackend.Application.Features.OrderDriverAssignments;

public class OffersProfile : Profile
{
    public OffersProfile()
    {
        CreateMap<HaitikBackend.Domain.Entities.OrderDriverAssignment, DriverOffer>()
            .ForMember(e => e.Latitude, e => e
            .MapFrom(e => e.Order.DeliveryLocation.CurrentLocation.Y))
            .ForMember(e => e.Longitude, e => e
            .MapFrom(e => e.Order.DeliveryLocation.CurrentLocation.X));
    }
}
