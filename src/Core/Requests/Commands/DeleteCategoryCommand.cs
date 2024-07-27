namespace AttachManagement.Core.Requests.Commands;

public record DeleteCategoryCommand(int Id)
    : IRequest<Result<bool, Error>>;
