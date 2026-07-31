using FluentValidation;

namespace HaitikBackend.Application.Features.DeliveryProofs.Commands.ProofDelivery;

public class ProofDeliveryValidator : AbstractValidator<ProofDeliveryCommand>
{
    public ProofDeliveryValidator()
    {
        RuleFor(e => e.file).Must(e => e.Validate()).WithMessage("The provided file is not valid");

        RuleFor(e => e.orderId).GreaterThan(0).WithMessage("The provided Id was not valid");

        RuleFor(e => e.reciverName).NotEmpty();
    }
}
