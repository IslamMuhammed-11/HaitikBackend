using HaitikBackend.Application.Common.Interfaces.OTP;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Infrastructure.Services.OTP;

public class OtpGenerator : IOtpGenerator
{
    public string GenerateOTP(int length = OtpCode.OtpLength)
        => throw new NotImplementedException();
}
