using FluentValidation;

namespace HaitikBackend.Application.Features.Drivers.Commands.UpdateTotalOrdersPerDay;

public class UpdateTotalOrdersPerDayValidator : AbstractValidator<UpdateTotalOrdersPerDayCommand>
{
    public UpdateTotalOrdersPerDayValidator()
    {
        RuleFor(x => x.DriverId).GreaterThan(0);
        RuleFor(x => x.MaximumOrdersPerDay).GreaterThanOrEqualTo((short)0).When(x => x.MaximumOrdersPerDay.HasValue);
    }
}
