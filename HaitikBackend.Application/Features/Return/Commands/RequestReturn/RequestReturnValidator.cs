using FluentValidation;

namespace HaitikBackend.Application.Features.Return.Commands.RequestReturn;

public class RequestReturnValidator : AbstractValidator<RequestReturnCommand>
{
    public RequestReturnValidator()
    {
        RuleFor(e => e.orderId).GreaterThan(0);
        RuleFor(e => e.reason).NotEmpty().NotNull().MinimumLength(2);
    }
}
