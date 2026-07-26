using System;
using System.Collections.Generic;

namespace HaitikBackend.Infrastructure;

public partial class DeliveryAdmin
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
