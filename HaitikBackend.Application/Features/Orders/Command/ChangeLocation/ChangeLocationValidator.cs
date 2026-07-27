using FluentValidation;

namespace HaitikBackend.Application.Features.Orders.Command.ChangeLocation;

public class ChangeLocationValidator : AbstractValidator<ChangeLocationCommand>
{
    public ChangeLocationValidator()
    {
        RuleFor(e => e.OrderId).NotNull().GreaterThan(0);
        RuleFor(e => e.NewLocation).NotNull().NotEmpty();
        RuleFor(e => e.NewLocation.CurrentLocation).NotEmpty().NotNull();
    }
}
