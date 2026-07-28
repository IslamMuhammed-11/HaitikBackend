namespace HaitikBackend.Domain.Entities;

public partial class RefreshToken : BaseEntity
{
    public int UserId { get; private set; }

    public string TokenHash { get; private set; } = null!;

    public DateTime Expiry { get; private set; }

    public DateTime? RevokedAt { get; private set; }

    private RefreshToken()
    {
    }

    private RefreshToken(int userId, string tokenHash, DateTime expiry, DateTime? revokedAt = null)
    {
        TokenHash = tokenHash;
        Expiry = expiry;
        RevokedAt = revokedAt;
    }

    internal static RefreshToken Create(int userId, string tokenHash, DateTime expiry)
    {
        return new RefreshToken(userId, tokenHash, expiry, null);
    }


    public void UpdateToken(string tokenHash, DateTime expiry)
    {
        TokenHash = tokenHash;
        Expiry = expiry;
        RevokedAt = null;
    }

    public void RevokeToken() => RevokedAt = DateTime.UtcNow;

    public virtual User User { get; private set; } = null!;
}
