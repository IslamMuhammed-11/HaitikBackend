using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Common.Interfaces.OTP;

public interface IOtpGenerator
{
    string GenerateOTP(int length = OtpCode.OtpLength);
}
