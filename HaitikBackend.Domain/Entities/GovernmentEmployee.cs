namespace HaitikBackend.Domain.Entities;

using HaitikBackend.Domain.Common.Results;

public partial class GovernmentEmployee : BaseEntity
{
    public int Id { get; private set; }

    public int UserId { get; private set; }

    public int AgencyId { get; private set; }


    private GovernmentEmployee()
    {
    }

    private GovernmentEmployee(int userId, int agencyId)
    {
        UserId = userId;
        AgencyId = agencyId;
    }

    internal static GovernmentEmployee Create(int userId, int agencyId)
    {
        return new GovernmentEmployee(userId, agencyId);

    }

    public virtual GovernmentAgency Agency { get; private set; } = null!;

    public virtual User User { get; private set; } = null!;
}
