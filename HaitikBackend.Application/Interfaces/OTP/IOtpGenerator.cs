namespace HaitikBackend.Application.Interfaces.OTP;

public interface IOtpGenerator
{
    string GenerateOTP(int length = 6);
}
