using AutoMapper;
using HaitikBackend.Application.Features.GovernmentEmployees.Queries.Responses;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Mapping;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<GovernmentEmployee, EmployeeDetails>()
            .ForMember(dest => dest.AgencyName, opt => opt
            .MapFrom(e => e.Agency.Name))
            .ForMember(dest => dest.FirstName, opt => opt
            .MapFrom(e => e.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt
            .MapFrom(e => e.User.LastName))
            .ForMember(dest => dest.Email, opt => opt
            .MapFrom(e => e.User.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt
            .MapFrom(e => e.User.PhoneNumber))
            .ReverseMap();
    }
}
