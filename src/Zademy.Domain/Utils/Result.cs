namespace Zademy.Domain.Utils;

public abstract class Result<T>
{
    public abstract bool IsSuccess { get; }
    public abstract bool IsFailure { get; }

    public abstract TResult Match<TResult>(Func<T, TResult> onSuccess, Func<string, TResult> onFailure);
    public abstract void When(Action<T> onSuccess, Action<string> onFailure);

    public static Result<T> Success(T value) => new SuccessResult<T>(value);
    public static Result<T> Failure(string error) => new FailureResult<T>(error);

    public static Result<T> Failure(string error, string errorCode) =>
        new FailureResult<T>(error) { ErrorCode = errorCode };
}

public class SuccessResult<T>(T value) : Result<T>
{
    public override bool IsSuccess => true;
    public override bool IsFailure => false;
    public T Value => value;

    public override TResult Match<TResult>(Func<T, TResult> onSuccess, Func<string, TResult> onFailure)
    {
        return onSuccess(value);
    }

    public override void When(Action<T> onSuccess, Action<string> onFailure)
    {
        onSuccess(value);
    }
}

public class FailureResult<T>(string error) : Result<T>
{
    public override bool IsSuccess => false;
    public override bool IsFailure => true;
    public string Error => error;
    public string? ErrorCode { get; init; }

    public override TResult Match<TResult>(Func<T, TResult> onSuccess, Func<string, TResult> onFailure)
    {
        return onFailure(error);
    }

    public override void When(Action<T> onSuccess, Action<string> onFailure)
    {
        onFailure(error);
    }
}