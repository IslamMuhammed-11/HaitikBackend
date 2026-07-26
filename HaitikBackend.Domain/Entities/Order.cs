using System;
using System.Collections.Generic;

namespace HaitikBackend.Infrastructure;

public partial class Order
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public int? AssignedDriver { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal PickupLat { get; set; }

    public decimal PickupLong { get; set; }

    public int? GeoZone { get; set; }

    public int AgencyId { get; set; }

    public virtual GovernmentAgency Agency { get; set; } = null!;

    public virtual ICollection<DeliveryProof> DeliveryProofs { get; set; } = new List<DeliveryProof>();

    public virtual GeoZone? GeoZoneNavigation { get; set; }

    public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; } = new List<OrderStatusHistory>();
}
