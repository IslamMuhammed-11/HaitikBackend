using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Application.Features.PublicTracking.TrackOrder;

public sealed record PublicTrackingResponse(
    int OrderId,
    enOrderStatus Status,
    DateTime CreatedAt);
