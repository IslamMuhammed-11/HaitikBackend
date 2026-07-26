using FluentValidation;

namespace HaitikBackend.Application.Features.Users.Command.ValidatorExtensions;

public static class UserValidationExtension
{
    public static IRuleBuilderOptions<T, string> RequiredName<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule.NotEmpty().WithMessage(UserValidationErrors.NameRequired);              
    }

    public static IRuleBuilderOptions<T, string> NameMaximumLength<T>(
        this IRuleBuilder<T, string> rule, int maxLength)
    {
        return rule.MaximumLength(maxLength).WithMessage(UserValidationErrors.NameTooLong);
    }

    public static IRuleBuilderOptions<T, string> EmailRequired<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule.NotEmpty().WithMessage(UserValidationErrors.EmailRequired);
    }

    public static IRuleBuilderOptions<T, string> EmailInvalid<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule.EmailAddress().WithMessage(UserValidationErrors.EmailInvalid);
    }
    
    public static IRuleBuilderOptions<T, string> PasswordRequired<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule.NotEmpty().WithMessage(UserValidationErrors.PasswordRequired);
    }

    public static IRuleBuilderOptions<T, string> PasswordMinimumLength<T>(
        this IRuleBuilder<T, string> rule, int minLength)
    {
        return rule.MinimumLength(minLength).WithMessage(UserValidationErrors.PasswordTooShort);
    }

    public static IRuleBuilderOptions<T,string?> OptionalName<T>(this IRuleBuilder<T, string?> rule)
    {
        return rule.MaximumLength(30).WithMessage(UserValidationErrors.NameTooLong);
    }

    public static IRuleBuilderOptions<T, string?> PhoneNumberMaximumLength<T>(this IRuleBuilder<T, string?> rule)
    {
        return rule.MaximumLength(15).WithMessage(UserValidationErrors.PhoneNumberTooLong);
    }
}
