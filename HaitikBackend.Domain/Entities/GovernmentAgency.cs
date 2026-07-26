using System;
using System.Collections.Generic;

namespace HaitikBackend.Infrastructure;

public partial class GovernmentAgency
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<GovernmentEmployee> GovernmentEmployees { get; set; } = new List<GovernmentEmployee>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
