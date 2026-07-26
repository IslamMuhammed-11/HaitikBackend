using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Users.Queries.GetUsersPage;

public sealed record GetUsersPageQuery(int pageNumber, int pageSize) : IRequest<Result<GetUsersPageResponse>>;

