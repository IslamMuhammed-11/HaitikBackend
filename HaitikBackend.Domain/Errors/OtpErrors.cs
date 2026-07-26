using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class OtpErrors
{
    public static Error OtpNotFound => Error.Create("Otp.NotFound", "OTP was not found.", enErrorTypes.NotFound);

    public static Error OtpExpired => Error.Create("Otp.Expired", "The OTP has expired.", enErrorTypes.Validation);

    public static Error OtpInvalid => Error.Create("Otp.Invalid", "The provided OTP is invalid.", enErrorTypes.InvalidCreds);

    public static Error MaxAttemptsReached => Error.Create("Otp.MaxAttempts", "Maximum OTP verification attempts have been reached.", enErrorTypes.Conflict);
}
