using AutoMapper;
using HaitikBackend.Application.Features.Return.Queries.Responses;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Mapping;

public class ReturnProfile : Profile
{
    public ReturnProfile()
    {
        CreateMap<Return, ReturnRequest>()
            .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(e => e.Agency.Name))
            .ForMember(dest => dest.ReviewdByName, opt => opt.MapFrom(e => e.User != null ? e.User.FirstName + " " + e.User.LastName : null));
    }

}
