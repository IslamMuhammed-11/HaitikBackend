using System.Collections.Generic;

namespace HaitikBackend.Application.Features.Return.Queries.Responses;

public sealed record ReturnsPageResponse(IReadOnlyCollection<ReturnRequest> Requests, int PageSize, int PageNumber, int TotalCount);
