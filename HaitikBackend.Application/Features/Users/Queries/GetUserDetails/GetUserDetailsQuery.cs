using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Users.Queries.GetUserDetails;

public sealed record GetUserDetailsQuery(int Id) : IRequest<Result<GetUserDetailsResponse>>;

