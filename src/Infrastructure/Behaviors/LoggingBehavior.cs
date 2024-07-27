namespace AttachManagement.Infrastructure.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result<TResponse, Error>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var typeName = request.GetGenericTypeName();

        _logger.LogInformation("Handling command {CommandName} ({@Command})", typeName, request);

        var response = await next();

        if (response.IsSuccess)
        {
            _logger.LogInformation("Command {CommandName} handled - response: {@Response}", typeName, response.Value());
        }
        else
        {
            _logger.LogInformation("Command {CommandName} failed - error: {@Error}", typeName, response.Error());
        }

        return response;
    }
}
