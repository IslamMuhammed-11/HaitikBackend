using System;
using System.Collections.Generic;

namespace HaitikBackend.Infrastructure;

public partial class OrderStatusHistory
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string LastStatus { get; set; } = null!;

    public string CurrentStatus { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;
}
