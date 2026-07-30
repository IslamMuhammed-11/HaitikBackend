using HaitikBackend.Domain.ValueObjects;

namespace HaitikBackend.Domain.Entities;

public partial class DriverLocationPing : BaseEntity
{

    public int DriverId { get; private set; }

    public GeoLocation Location { get; private set; } = null!;

    public DateTime Timestamp { get; private set; }

    private DriverLocationPing()
    {
    }

    private DriverLocationPing(int driverId, GeoLocation currentLocation, DateTime timestamp)
    {
        DriverId = driverId;
        Location = currentLocation;
        Timestamp = timestamp;
    }

    public static DriverLocationPing Create(int driverId, GeoLocation curremtLocation, DateTime timestamp)
    {
        return new DriverLocationPing(driverId, curremtLocation, timestamp);

    }

    public void UpdateLocation(GeoLocation location, DateTime timestamp)
    {
        Location = location;
        Timestamp = timestamp;
    }

    public virtual Driver Driver { get; private set; } = null!;
}
