using FluentValidation;

namespace HaitikBackend.Application.Features.Drivers.Commands.SetDriverAsOnline;

public class SetDriverAsOnlineValidator : AbstractValidator<SetDriverAsOnlineCommand>
{
    public SetDriverAsOnlineValidator()
    {
        RuleFor(x => x.DriverId).GreaterThan(0);
    }
}
