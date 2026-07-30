using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using MediatR;

namespace HaitikBackend.Application.Features.OrderStatusHistories.Queries.GetOrderStatusHistoriesPage;

public sealed record GetOrderStatusHistoriesPageQuery( int PageSize, int PageNumber) : IRequest<Result<HaitikBackend.Application.Features.OrderStatusHistories.Queries.Responses.OrderStatusHistoriesPageResponse>>;
