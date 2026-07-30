using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Application.Features.OrderStatusHistories.Queries.Responses;

public sealed record OrderStatusHistoryResponse(int Id, int OrderId, enOrderStatus LastStatus, enOrderStatus CurrentStatus, DateTime UpdatedAt);
