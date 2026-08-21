using HaitikBackend.Application.Abstractions;
using PhoneNumbers;
namespace HaitikBackend.Infrastructure.Implementaions;

public class PhoneNumberChecker : IPhoneNumberChecker
{
    public bool CheckPhoneNumber(string phoneNumber, string? region = null)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();

        var number = phoneNumberUtil.Parse(phoneNumber, region);

        return phoneNumberUtil.IsValidNumber(number);
    }
}
