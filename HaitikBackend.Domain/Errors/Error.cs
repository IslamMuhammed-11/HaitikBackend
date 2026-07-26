using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public class Error
{
    public string Code { get; private set; } = string.Empty;

    public string Message { get; private set; } = string.Empty;

    public enErrorTypes ErrorTypes { get; private set; }

    private Error(string code, string message, enErrorTypes errorTypes)
    {
        Code = code;
        Message = message;
        this.ErrorTypes = errorTypes;
    }

    internal static Error Create(string code, string message , enErrorTypes errorTypes)
    {
        return new Error(code, message , errorTypes);
    }

}

