using HaitikBackend.Domain.ValueObjects;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Queries.Responses;

public class DriverOffer
{
    public DriverOffer(int orderId, GeoLocation pickupLocation)
    {
        OrderId = orderId;
        PickupLocation = pickupLocation;
    }

    public int OrderId { get; init; }

    public GeoLocation PickupLocation { get; init; }
}
