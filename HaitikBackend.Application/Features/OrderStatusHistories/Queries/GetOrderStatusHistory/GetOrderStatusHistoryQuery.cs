using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.OrderStatusHistories.Queries.GetOrderStatusHistory;

public sealed record GetOrderStatusHistoryQuery(int OrderId) : IRequest<Result<HaitikBackend.Application.Features.OrderStatusHistories.Queries.Responses.OrderStatusHistoryResponse>>;
