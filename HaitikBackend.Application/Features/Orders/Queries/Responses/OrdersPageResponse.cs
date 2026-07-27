using System.Collections.Generic;

namespace HaitikBackend.Application.Features.Orders.Queries.Responses;

public sealed record OrdersPageResponse(IReadOnlyCollection<OrderDetails> Orders, int PageSize, int PageNumber , int TotalCount);
