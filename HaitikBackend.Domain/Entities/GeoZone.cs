using System;
using System.Collections.Generic;

namespace HaitikBackend.Infrastructure;

public partial class GeoZone
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
