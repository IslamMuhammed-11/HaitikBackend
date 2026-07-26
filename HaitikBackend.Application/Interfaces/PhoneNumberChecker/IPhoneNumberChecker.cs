namespace HaitikBackend.Application.Interfaces.PhoneNumberChecker;

public interface IPhoneNumberChecker
{
    bool CheckPhoneNumber(string phoneNumber, string? region = null);
}
