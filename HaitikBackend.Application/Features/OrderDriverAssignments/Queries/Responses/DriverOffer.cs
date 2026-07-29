using HaitikBackend.Domain.ValueObjects;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Queries.Responses;

public class DriverOffer
{
    public DriverOffer(int orderId, GeoLocation pickupLocation, DateTime cratedAt, DateTime? respondedAt)
    {
        OrderId = orderId;
        PickupLocation = pickupLocation;
        CreatedAt = cratedAt;
        RespondedAt = respondedAt;
    }

    private DriverOffer()
    {
    }

    public int OrderId { get; init; }

    public GeoLocation PickupLocation { get; init; } = null!;

    public DateTime CreatedAt { get; init; }

    public DateTime? RespondedAt { get; init; }

}
