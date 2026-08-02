using NetTopologySuite.Geometries;

namespace HaitikBackend.Domain.ValueObjects;

public sealed record GeoLocation
{
    public Point CurrentLocation { get; set; }

    public GeoLocation(Point currentLocation)
    {
        CurrentLocation = currentLocation;
        CurrentLocation.SRID = 4326; // Set the SRID to 4326 for WGS84
    }
}
