using AutoMapper;
using HaitikBackend.Application.Features.Drivers.Queries.Responses;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Mapping;

public class DriverProfile : Profile
{
    public DriverProfile()
    {
        CreateMap<Driver, DriverDetails>()
            .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(e => e.User.FirstName + " " + e.User.LastName))
            .ForMember(dest => dest.TotalActiveOrders, opt => opt.MapFrom(e => e.Orders.Count(e => e.Status != Domain.Enums.enOrderStatus.Delivered)));
    }
}
