namespace HaitikBackend.Application.Features.DeliveryProofs.Queries.Responses;

public sealed record DeliveryProofResponse(int Id, int OrderId, string ImageUrl, string ReciverName, string? DeliveryNotes, DateTime DeliverdAt);
