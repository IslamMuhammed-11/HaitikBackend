using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Abstractions;

public interface IOtpGenerator
{
    string GenerateOTP(int length = OtpCode.OtpLength);
}
