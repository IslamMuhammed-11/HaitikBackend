using FluentValidation;

namespace HaitikBackend.Application.Features.Orders.Command.AssignDriver;

public class AssignDriverValidator : AbstractValidator<AssignDriverCommand>
{
    public AssignDriverValidator()
    {
        RuleFor(e => e.OrderId).NotNull().GreaterThan(0);
        RuleFor(e => e.DriverId).NotNull().GreaterThan(0);
    }
}
