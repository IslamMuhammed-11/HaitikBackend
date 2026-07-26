using FluentValidation;
using HaitikBackend.Application.Features.Users.Command.ValidatorExtensions;

namespace HaitikBackend.Application.Features.Users.Command.UpdatePhoneNumber;

public class UpdatePhoneNumberValidator : AbstractValidator<UpdatePhoneNumberCommand>
{
    public UpdatePhoneNumberValidator()
    {
        RuleFor(e => e.PhoneNumber).PhoneNumberMaximumLength();
        RuleFor(e => e.Id).GreaterThan(0);
    }
}
