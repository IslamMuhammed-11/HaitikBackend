using HaitikBackend.Domain.Errors;

namespace HaitikBackend.Domain.Common.Results;

public class Result
{
    public bool IsSuccess { get; private set; }

    public Error? Error { get; private set; }

    protected Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new Result(true, null);

    public static Result Failed(Error error) => new Result(false, error);
}
