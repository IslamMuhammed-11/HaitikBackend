using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Roles.Queries.GetRolesPage;

public sealed record GetRolesPageQuery() : IRequest<Result<HaitikBackend.Application.Features.Roles.Queries.Responses.RolesPageResponse>>;
