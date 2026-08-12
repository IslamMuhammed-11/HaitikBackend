using HaitikBackend.Application.Common.Interfaces.PhoneNumberChecker;
using PhoneNumbers;
namespace HaitikBackend.Infrastructure.Services.PhoneNumber;

public class PhoneNumberChecker : IPhoneNumberChecker
{
    public bool CheckPhoneNumber(string phoneNumber, string? region = null)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();

        var number = phoneNumberUtil.Parse(phoneNumber, region);

        return phoneNumberUtil.IsValidNumber(number);
    }
}
