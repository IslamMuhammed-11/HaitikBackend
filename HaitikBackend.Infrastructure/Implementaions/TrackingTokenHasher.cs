using System.Security.Cryptography;
using System.Text;
using HaitikBackend.Application.Abstractions;

namespace HaitikBackend.Infrastructure.Implementaions;

public sealed class TrackingTokenHasher : ITrackingTokenHasher
{
    public string Hash(string token)
    {
        var tokenBytes = Encoding.UTF8.GetBytes(token);
        var hashBytes = SHA256.HashData(tokenBytes);

        return Convert.ToHexString(hashBytes);
    }

    public bool Verify(string token, string hash)
    {
        var providedHash = Hash(token);
        var providedHashBytes = Encoding.ASCII.GetBytes(providedHash);
        var storedHashBytes = Encoding.ASCII.GetBytes(hash);

        return CryptographicOperations.FixedTimeEquals(providedHashBytes, storedHashBytes);
    }
}
