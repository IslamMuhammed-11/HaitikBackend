using FluentValidation;
using HaitikBackend.Application.Features.Users.Command.ValidatorExtensions;

namespace HaitikBackend.Application.Features.Orders.Command.PlaceOrder;

public class PlaceOrderValidator : AbstractValidator<PlaceOrderCommand>
{
    public PlaceOrderValidator()
    {
        RuleFor(e => e.CustomerPhoneNumber).PhoneNumberMaximumLength().NotEmpty().NotNull();
        //RuleFor(e => e.PickupLocation).NotNull().NotEmpty();
        RuleFor(e => e.AgencyId).NotNull().GreaterThan(0);
    }
}
