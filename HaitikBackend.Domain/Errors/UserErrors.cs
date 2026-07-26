using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class UserErrors
{
    public static Error EmailAlreadyExists => Error.Create("User.EmailExists",
        "User with the provided email already exists.", enErrorTypes.Conflict);

    public static Error UserNotFound(int id) => Error.Create("User.NotFound", $"User With this Id was not Found : {id}", enErrorTypes.NotFound);

    public static Error InvalidCreds => Error.Create("User.InvalidCreds", "The Provided Credntials Are not Valid", enErrorTypes.InvalidCreds);

    public static Error InvalidPhoneNumber => Error.Create("User.InvalidPhoneNumber", "The Provided Phone Number Is not Valid", enErrorTypes.Validation);


}
