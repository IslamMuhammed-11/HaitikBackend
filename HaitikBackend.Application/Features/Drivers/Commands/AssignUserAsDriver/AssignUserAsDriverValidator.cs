using FluentValidation;

namespace HaitikBackend.Application.Features.Drivers.Commands.AssignUserAsDriver;

public class AssignUserAsDriverValidator : AbstractValidator<AssignUserAsDriverCommand>
{
    public AssignUserAsDriverValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).NotNull();
        RuleFor(x => x.GeoZoneId).GreaterThan(0).NotNull();
        RuleFor(x => x.MaximumOrderPerDay).GreaterThanOrEqualTo((short)0).When(x => x.MaximumOrderPerDay.HasValue);
    }
}
