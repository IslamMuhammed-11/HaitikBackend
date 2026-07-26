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

    public static Result<GovernmentAgency> Create(string name)
    {
        var agency = new GovernmentAgency(name);

        return Result<GovernmentAgency>.Success(agency);
    }

    public virtual ICollection<GovernmentEmployee> GovernmentEmployees { get; private set; } = new List<GovernmentEmployee>();

    public virtual ICollection<Order> Orders { get; private set; } = new List<Order>();
}
