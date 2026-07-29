using System.Collections.Generic;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Queries.Responses;

public sealed record OffersPageResponse(IReadOnlyCollection<DriverOffer> Offers, int PageSize, int PageNumber, int TotalCount);
