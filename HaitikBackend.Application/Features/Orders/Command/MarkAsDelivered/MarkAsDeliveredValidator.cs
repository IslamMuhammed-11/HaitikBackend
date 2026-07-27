using FluentValidation;

namespace HaitikBackend.Application.Features.Orders.Command.MarkAsDelivered;

public class MarkAsDeliveredValidator : AbstractValidator<MarkAsDeliveredCommand>
{
    public MarkAsDeliveredValidator()
    {
        RuleFor(e => e.OrderId).NotNull().GreaterThan(0);
    }
}
