using System;
using System.Collections.Generic;

namespace HaitikBackend.Infrastructure;

public partial class DriverLocationPing
{
    public int Id { get; set; }

    public int DriverId { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public byte[] Timestamp { get; set; } = null!;

    public virtual Driver Driver { get; set; } = null!;
}
