using System;
using System.Collections.Generic;

namespace HaitikBackend.Infrastructure;

public partial class GovernmentEmployee
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int AgencyId { get; set; }

    public virtual GovernmentAgency Agency { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
