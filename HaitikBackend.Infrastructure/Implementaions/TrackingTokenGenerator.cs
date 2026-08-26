using System.Security.Cryptography;
using HaitikBackend.Application.Abstractions;

namespace HaitikBackend.Infrastructure.Implementaions;

public sealed class TrackingTokenGenerator : ITrackingTokenGenerator
{
    public string Generate()
    {
        Span<byte> tokenBytes = stackalloc byte[32];
        RandomNumberGenerator.Fill(tokenBytes);

        return Convert.ToBase64String(tokenBytes)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }
}
