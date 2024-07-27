namespace AttachManagement.Core.Common;

public class Result<TValue, TError>
{
    private readonly TValue? _value;
    private readonly TError? _error;

    private Result(TValue value)
    {
        IsSuccess = true;
        _value = value;
        _error = default;
    }

    private Result(TError error)
    {
        IsSuccess = false;
        _error = error;
        _value = default;
    }

    public static implicit operator Result<TValue, TError>(TValue value) => new(value);

    public static implicit operator Result<TValue, TError>(TError error) => new(error);

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public TValue? Value() => IsSuccess
        ? _value
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public TError? Error() => IsFailure
        ? _error
        : throw new InvalidOperationException("The error of a success result can not be accessed.");
}
