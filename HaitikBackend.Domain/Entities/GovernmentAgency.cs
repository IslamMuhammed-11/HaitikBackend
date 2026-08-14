namespace HaitikBackend.Domain.Entities;

using HaitikBackend.Domain.ValueObjects;

public partial class GovernmentAgency : BaseEntity
{
    public int Id { get; private set; }

    public string Name { get; private set; } = null!;

    public GeoLocation Location { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;


    private GovernmentAgency()
    {
    }

    private GovernmentAgency(string name, GeoLocation location, string email, string passwordHash)
    {
        Name = name;
        Location = location;
        Email = email;
        PasswordHash = passwordHash;
    }

    public static GovernmentAgency Create(string name, GeoLocation location, string email, string passwordHash)
    {
        return new GovernmentAgency(name, location, email, passwordHash);
    }


    public void CreateRefreshToken(string tokenHash, DateTime expiry)
    {
        var token = RefreshToken.Create(null, Id, tokenHash, expiry);

        RefreshTokens.Add(token);
    }

    public virtual ICollection<Order> Orders { get; private set; } = new List<Order>();

    public virtual ICollection<Return> Returns { get; private set; } = new List<Return>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();
}
