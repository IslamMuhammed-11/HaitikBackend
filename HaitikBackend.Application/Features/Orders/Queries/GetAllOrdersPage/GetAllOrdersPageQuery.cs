using HaitikBackend.Application.Features.Orders.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Queries.GetAllOrdersPage;

public sealed record GetAllOrdersPageQuery(enOrderStatus? Status, int PageSize, int PageNumber) : IRequest<Result<OrdersPageResponse>>;
