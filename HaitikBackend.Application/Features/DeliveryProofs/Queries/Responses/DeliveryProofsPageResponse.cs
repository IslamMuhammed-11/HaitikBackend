using System.Collections.Generic;

namespace HaitikBackend.Application.Features.DeliveryProofs.Queries.Responses;

public sealed record DeliveryProofsPageResponse(IReadOnlyCollection<DeliveryProofResponse> Items, int PageSize, int PageNumber, int TotalCount);
