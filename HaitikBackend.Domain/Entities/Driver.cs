using System;
using System.Collections.Generic;

namespace HaitikBackend.Infrastructure;

public partial class Driver
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Status { get; set; } = null!;

    public int GeoZoneId { get; set; }

    public virtual ICollection<DriverLocationPing> DriverLocationPings { get; set; } = new List<DriverLocationPing>();

    public virtual GeoZone GeoZone { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
