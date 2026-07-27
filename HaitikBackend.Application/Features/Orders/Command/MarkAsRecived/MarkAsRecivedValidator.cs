using FluentValidation;

namespace HaitikBackend.Application.Features.Orders.Command.MarkAsRecived;

public class MarkAsRecivedValidator : AbstractValidator<MarkAsRecivedCommand>
{
    public MarkAsRecivedValidator()
    {
        RuleFor(e => e.OrderId).NotNull().GreaterThan(0);
    }
}
