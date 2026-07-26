using System;
using System.Collections.Generic;

namespace HaitikBackend.Infrastructure;

public partial class DeliveryProof
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string ReciverName { get; set; } = null!;

    public string DeliveryNotes { get; set; } = null!;

    public DateTime DeliverdAt { get; set; }

    public virtual Order Order { get; set; } = null!;
}
