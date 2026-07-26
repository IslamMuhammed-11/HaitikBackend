using HaitikBackend.Domain.ValueObjects;
using HaitikBackend.Domain.Common.Results;

namespace HaitikBackend.Domain.Entities;

public partial class GeoZone : BaseEntity
{
    public int Id { get; private set; }

    public string Name { get; private set; } = null!;

    public Area Polygon { get; private set; } = null!;


    private GeoZone()
    {
    }

    private GeoZone(string name, Area polygon)
    {
        Name = name;
        Polygon = polygon;
    }


    public static Result<GeoZone> Create(string name, Area polygon)
    {
        var zone = new GeoZone(name, polygon);

        return Result<GeoZone>.Success(zone);
    }

   


    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
