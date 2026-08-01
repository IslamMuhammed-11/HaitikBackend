using AutoMapper;
using HaitikBackend.Application.Features.Roles.Queries.Responses;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Features.Roles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDetails>();
    }
}
