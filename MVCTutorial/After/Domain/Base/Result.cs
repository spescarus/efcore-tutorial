namespace Domain.Base;

public class Result
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, string error)
    {
        switch (isSuccess)
        {
            case true when error != string.Empty:
                throw new InvalidOperationException();
            case false when error == string.Empty:
                throw new InvalidOperationException();
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }
    public static Result Failure(string message)
    {
        return new Result(false, message);
    }

    public static Result<T> Failure<T>(string message)
    {
        return new Result<T>(default, false, message);
    }

    public static Result Success()
    {
        return new Result(true, string.Empty);
    }

    public static Result<T> Success<T>(T value)
    {
        return new Result<T>(value, true, string.Empty);
    }
}


public class Result<T> : Result
{
    private readonly T _value;

    public T Value
    {
        get
        {
            if (!IsSuccess)
                throw new InvalidOperationException();

            return _value;
        }
    }

    protected internal Result(T? value, bool isSuccess, string error)
        : base(isSuccess, error)
    {
        _value = value;
    }
}