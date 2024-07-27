namespace AttachManagement.Core.Common;

public record Error(string Code, string Message, ErrorType Type)
{
    public static Error Failure(string code, string message) => new(code, message, ErrorType.Failure);

    public static Error Invalid(string code, string message) => new(code, message, ErrorType.Invalid);

    public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);

    public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);
}