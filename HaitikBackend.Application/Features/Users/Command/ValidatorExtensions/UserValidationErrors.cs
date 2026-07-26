namespace HaitikBackend.Application.Features.Users.Command.ValidatorExtensions;

public static class UserValidationErrors
{
    public const string NameRequired = "Name is Required";
    public const string NameTooLong = "Name Cannot Excced 30 chars";

    public const string EmailRequired = "Email is Required";
    public const string EmailInvalid = "The Email is not formatted right";

    public const string PasswordRequired = "Password is Required";
    public const string PasswordTooShort = "Password should be more than 8 chars";

    public const string PhoneNumberTooLong = "Phone number should not excced 15 chars";
}
