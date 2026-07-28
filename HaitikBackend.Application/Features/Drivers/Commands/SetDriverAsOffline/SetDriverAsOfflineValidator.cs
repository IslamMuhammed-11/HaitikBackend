using FluentValidation;

namespace HaitikBackend.Application.Features.Drivers.Commands.SetDriverAsOffline;

public class SetDriverAsOfflineValidator : AbstractValidator<SetDriverAsOfflineCommand>
{
    public SetDriverAsOfflineValidator()
    {
        RuleFor(x => x.DriverId).GreaterThan(0);
    }
}
