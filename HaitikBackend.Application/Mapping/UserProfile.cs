using AutoMapper;
using HaitikBackend.Application.Features.Users.Queries.GetUserDetails;
using HaitikBackend.Application.Features.Users.Queries.GetUsersPage;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, GetUserDetailsResponse>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(e => e.FirstName + e.LastName))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(e => e.Role.Name))
            .ReverseMap();

        CreateMap<User, UserDetails>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(e => e.FirstName + e.LastName))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(e => e.Role.Name))
            .ReverseMap();
    }
}
