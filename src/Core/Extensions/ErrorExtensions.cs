namespace AttachManagement.Core.Extensions;

public static class ErrorExtensions
{
    public static Error ToError(this Exception exception) =>
        exception is BaseException baseException
        ? baseException.Error!
        : Error.Failure(exception.GetType().Name, exception.Message);
}
