using FluentValidation;

namespace HaitikBackend.Application.Features.Return.Commands.AcceptReturn;

public class AcceptReturnValidator : AbstractValidator<AcceptReturnCommand>
{
    public AcceptReturnValidator()
    {
        RuleFor(e => e.userId).NotEmpty().NotNull();
        RuleFor(e => e.orderId).NotEmpty().NotNull();
    }
}
