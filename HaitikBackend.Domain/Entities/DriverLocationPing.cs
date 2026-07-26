using HaitikBackend.Domain.ValueObjects;
using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Domain.Entities;

public partial class DriverLocationPing : BaseEntity
{
    public int Id { get; private set; }

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

    public static Result<DriverLocationPing> Create(int driverId, GeoLocation curremtLocation, DateTime timestamp)
    {
        var ping = new DriverLocationPing(driverId, curremtLocation, timestamp);

        return Result<DriverLocationPing>.Success(ping);
    }

    public void UpdateLocation(GeoLocation location)
    {
        Location = location;
        Timestamp = DateTime.UtcNow;
    }

    public virtual Driver Driver { get; private set; } = null!;
}
