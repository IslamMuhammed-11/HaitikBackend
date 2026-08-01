using AutoMapper;
using HaitikBackend.Application.Features.Orders.Queries.Responses;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Features.Orders;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDetails>()
            .ForMember(dest => dest.AgencyName, opt => opt
            .MapFrom(e => e.Agency.Name))
            .ReverseMap();
    }
}
