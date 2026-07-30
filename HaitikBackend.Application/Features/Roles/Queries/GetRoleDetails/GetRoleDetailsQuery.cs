using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Roles.Queries.GetRoleDetails;

public sealed record GetRoleDetailsQuery(int Id) : IRequest<Result<HaitikBackend.Application.Features.Roles.Queries.Responses.RoleDetails>>;
