using XoDotNet.Infrastructure.Errors;

namespace XoDotNet.Infrastructure.Results;

public class Result
{
    public Result(bool isSuccessful, Error? error = default)
    {
        IsSuccess = isSuccessful;
        if (error is not null)
            Error = error;
    }

    public Result(bool isSuccessful, string error) : this(isSuccessful, new Error(error))
    {
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }

    public static Result Success()
    {
        return new Result(true);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

    public static Result Failure(string error)
    {
        return Failure(new Error(error));
    }
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? val, bool isSuccessful, Error? error = default)
        : base(isSuccessful, error)
    {
        _value = val;
    }

    public Result(TValue? val, bool isSuccessful, string error) : this(val, isSuccessful, new Error(error))
    {
    }

    public TValue? Value => IsSuccess
        ? _value
        : throw new Exception(Error?.Message);

    public static Result<TValue> Success(TValue val)
    {
        return new Result<TValue>(val, true);
    }

    public new static Result<TValue> Failure(Error error)
    {
        return new Result<TValue>(default, false, error);
    }

    public new static Result<TValue> Failure(string error)
    {
        return Failure(new Error(error));
    }
}