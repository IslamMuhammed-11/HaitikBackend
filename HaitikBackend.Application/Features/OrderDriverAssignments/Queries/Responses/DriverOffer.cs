using NetTopologySuite.Geometries;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Queries.Responses;

public class DriverOffer
{
    public DriverOffer(int orderId, Point pickupLocation, DateTime cratedAt, DateTime? respondedAt)
    {
        OrderId = orderId;
        Latitude = pickupLocation.Y;
        Longitude = pickupLocation.X;
        CreatedAt = cratedAt;
        RespondedAt = respondedAt;
    }

    private DriverOffer()
    {
    }

    public int OrderId { get; init; }

    public double Latitude { get; init; }

    public double Longitude { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? RespondedAt { get; init; }

}
