using HaitikBackend.Application.Abstractions;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Infrastructure.Implementaions;

public class OtpGenerator : IOtpGenerator
{
    public string GenerateOTP(int length = OtpCode.OtpLength)
        => throw new NotImplementedException();
}
