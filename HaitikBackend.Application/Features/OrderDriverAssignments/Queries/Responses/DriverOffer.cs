using HaitikBackend.Domain.ValueObjects;
using NetTopologySuite.Geometries;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Queries.Responses;

public class DriverOffer
{
    public DriverOffer(int orderId, Point pickupLocation, DateTime cratedAt, DateTime? respondedAt)
    {
        OrderId = orderId;
        PickupLocation = GeoLocation.Create(pickupLocation.Y ,pickupLocation.X);
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
