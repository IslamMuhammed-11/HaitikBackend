using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Interfaces.OTP;

public interface IOtpGenerator
{
    string GenerateOTP(int length = OtpCode.OtpLength);
}
