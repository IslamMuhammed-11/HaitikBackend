using AutoMapper;
using HaitikBackend.Application.Features.Orders.Queries.Responses;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Mapping;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDetails>()
            .ForMember(dest => dest.EmployeeName, opt => opt
            .MapFrom(e => e.GovernmentEmployee.User.FullName()))
            .ForMember(dest => dest.AgencyName, opt => opt
            .MapFrom(e => e.GovernmentEmployee.Agency.Name))
            .ReverseMap();
    }
}
