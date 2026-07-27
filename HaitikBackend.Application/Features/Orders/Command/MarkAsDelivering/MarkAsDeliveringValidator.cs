using FluentValidation;

namespace HaitikBackend.Application.Features.Orders.Command.MarkAsDelivering;

public class MarkAsDeliveringValidator : AbstractValidator<MarkAsDeliveringCommand>
{
    public MarkAsDeliveringValidator()
    {
        RuleFor(e => e.OrderId).NotNull().GreaterThan(0);
    }
}
