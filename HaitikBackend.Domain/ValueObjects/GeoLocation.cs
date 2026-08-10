using NetTopologySuite.Geometries;

namespace HaitikBackend.Domain.ValueObjects;

public sealed record GeoLocation
{
    public Point CurrentLocation { get; private set; }

    private GeoLocation(Point currentLocation)
    {
        CurrentLocation = currentLocation;
        CurrentLocation.SRID = 4326; // Set the SRID to 4326 for WGS84
    }




    public static GeoLocation Create(double latitude, double longitude)
    {
        Point point = new Point(longitude , latitude);

        return new GeoLocation(point);
    }


}
