using FluentValidation;

namespace HaitikBackend.Application.Features.Otp.CreateOtp;

public class CreateOtpValidator : AbstractValidator<CreateOtpCommand>
{
    public CreateOtpValidator()
    {
        RuleFor(e => e.OrderId).NotEmpty().GreaterThan(0);

        RuleFor(e => e.Purpose).NotEmpty();
    }
}
