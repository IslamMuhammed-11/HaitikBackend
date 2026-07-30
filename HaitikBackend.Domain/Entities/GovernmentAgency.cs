namespace HaitikBackend.Domain.Entities;

using HaitikBackend.Domain.ValueObjects;

public partial class GovernmentAgency : BaseEntity
{
    public int Id { get; private set; }

    public string Name { get; private set; } = null!;

    public GeoLocation Location { get; private set; } = null!;

    public string Username { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;


    private GovernmentAgency()
    {
    }

    private GovernmentAgency(string name, GeoLocation location, string username, string passwordHash)
    {
        Name = name;
        Location = location;
        Username = username;
        PasswordHash = passwordHash;
    }

    public static GovernmentAgency Create(string name, GeoLocation location, string username, string passwordHash)
    {
        return new GovernmentAgency(name, location, username, passwordHash);
    }


    public virtual ICollection<Order> Orders { get; private set; } = new List<Order>();

    public virtual ICollection<Return> Returns { get; private set; } = new List<Return>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();
}
