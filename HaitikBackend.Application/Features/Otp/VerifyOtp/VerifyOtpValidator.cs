using FluentValidation;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Features.Otp.VerifyOtp;

public class VerifyOtpValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpValidator()
    {
        RuleFor(e => e.Otp).NotEmpty().NotNull().Length(OtpCode.OtpLength);
        RuleFor(e => e.Purpose).NotEmpty().NotNull();
    }
}
