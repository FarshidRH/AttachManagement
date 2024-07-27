namespace AttachManagement.Core.Requests.Commands;

public record UpdateCategoryCommand(int Id, string Title)
    : IRequest<Result<CategoryDto, Error>>;
