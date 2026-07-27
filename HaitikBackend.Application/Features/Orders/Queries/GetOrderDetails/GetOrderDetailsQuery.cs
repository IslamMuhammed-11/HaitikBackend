using HaitikBackend.Application.Features.Users.Queries.GetUsersPage;
using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Queries.GetOrderDetails;

public sealed record GetOrderDetailsQuery(int Id) : IRequest<Result<UserDetails>>;
