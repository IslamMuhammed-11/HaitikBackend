using System.Collections.Generic;

namespace HaitikBackend.Application.Features.OrderStatusHistories.Queries.Responses;

public sealed record OrderStatusHistoriesPageResponse(IReadOnlyCollection<OrderStatusHistoryResponse> Items, int PageSize, int PageNumber, int TotalCount);
