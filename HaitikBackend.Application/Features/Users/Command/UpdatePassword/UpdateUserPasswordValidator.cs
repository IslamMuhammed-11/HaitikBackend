using FluentValidation;
using HaitikBackend.Application.Features.Users.Command.ValidatorExtensions;


namespace HaitikBackend.Application.Features.Users.Command.UpdatePassword;

public class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPasswordCommand>
{
    public UpdateUserPasswordValidator()
    {
        RuleFor(e => e.NewPassword).PasswordRequired().PasswordMinimumLength(8);
        RuleFor(e => e.CurrentPassword).PasswordRequired();
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}
