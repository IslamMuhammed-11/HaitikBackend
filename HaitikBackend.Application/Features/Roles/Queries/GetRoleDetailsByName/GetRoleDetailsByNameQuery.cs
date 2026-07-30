using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Roles.Queries.GetRoleDetailsByName;

public sealed record GetRoleDetailsByNameQuery(string Name) : IRequest<Result<HaitikBackend.Application.Features.Roles.Queries.Responses.RoleDetails>>;
