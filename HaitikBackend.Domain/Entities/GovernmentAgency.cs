namespace HaitikBackend.Domain.Entities;

using HaitikBackend.Domain.Common.Results;

public partial class GovernmentAgency : BaseEntity
{
    public int Id { get; private set; }

    public string Name { get; private set; } = null!;

    private GovernmentAgency()
    {
    }

    private GovernmentAgency(string name)
    {
        Name = name;
    }

    public static GovernmentAgency Create(string name)
    {
        return new GovernmentAgency(name);
    }

    public virtual ICollection<GovernmentEmployee> GovernmentEmployees { get; private set; } = new List<GovernmentEmployee>();

    public virtual ICollection<Order> Orders { get; private set; } = new List<Order>();
}
