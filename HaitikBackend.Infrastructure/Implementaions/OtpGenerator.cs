using HaitikBackend.Application.Abstractions;
using HaitikBackend.Domain.Entities;
using System.Security.Cryptography;

namespace HaitikBackend.Infrastructure.Implementaions;

public class OtpGenerator : IOtpGenerator
{
    public string GenerateOTP(int length = OtpCode.OtpLength)
    {
        return RandomNumberGenerator.GetInt32(0 , 1000000).ToString("D6");
    }
}
