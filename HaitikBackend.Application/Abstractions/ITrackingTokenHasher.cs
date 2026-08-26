namespace HaitikBackend.Application.Abstractions;

public interface ITrackingTokenHasher
{
    string Hash(string token);

    bool Verify(string token, string hash);
}
