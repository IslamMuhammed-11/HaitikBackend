using FluentValidation;
using HaitikBackend.Application.Features.Users.Command.ValidatorExtensions;

namespace HaitikBackend.Application.Features.GovernmentAgencies.Commands.AddAgency;

public class RegisterAgencyValidator : AbstractValidator<RegisterAgencyCommand>
{
    public RegisterAgencyValidator()
    {
        RuleFor(e => e.Name).NotNull().NotEmpty().MinimumLength(2);
        //RuleFor(e => e.Location).NotNull();
        RuleFor(e => e.Username).NotNull().MinimumLength(2);
        RuleFor(e => e.Password).PasswordRequired().PasswordMinimumLength(6);
    }
}
