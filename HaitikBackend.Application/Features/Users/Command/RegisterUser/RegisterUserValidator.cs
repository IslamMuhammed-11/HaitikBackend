using FluentValidation;
using HaitikBackend.Application.Features.Users.Command.ValidatorExtensions;

namespace HaitikBackend.Application.Features.Users.Command.RegisterUser;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(command => command.FirstName)
            .RequiredName().NameMaximumLength(30);

        RuleFor(command => command.LastName)
            .RequiredName().NameMaximumLength(30);

        RuleFor(command => command.Email)
            .EmailRequired().EmailInvalid();

        RuleFor(command => command.Password)
            .PasswordRequired().PasswordMinimumLength(8);

        RuleFor(command => command.PhoneNumber)
            .PhoneNumberMaximumLength();

    }
}

