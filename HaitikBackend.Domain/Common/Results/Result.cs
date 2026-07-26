using HaitikBackend.Domain.Errors;
#pragma warning disable CS0108
namespace HaitikBackend.Domain.Common.Results;

public class Result<T> : Result
{
    public T? Value { get; private set; }

    private Result(T? value, bool isSuccess, Error? error) : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new Result<T>(value, true, null);

    public static Result<T> Failed(Error error) => new Result<T>(default, false, error);
}
