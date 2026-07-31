using FluentValidation;

namespace HaitikBackend.Application.Features.DeliveryProofs.Commands.UpdateDeliveryNotes;

public class UpdateDeliveryNotesValidator : AbstractValidator<UpdateDeliveryNotesCommand>
{
    public UpdateDeliveryNotesValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("Invalid order id");
        RuleFor(x => x.DeliveryNotes).NotNull().WithMessage("Delivery notes must be provided");
    }
}
