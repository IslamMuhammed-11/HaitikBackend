namespace HaitikBackend.Application.Abstractions;

public interface IPhoneNumberChecker
{
    bool CheckPhoneNumber(string phoneNumber, string? region = null);
}
