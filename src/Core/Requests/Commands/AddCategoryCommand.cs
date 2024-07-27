namespace AttachManagement.Core.Requests.Commands;

public record AddCategoryCommand(string Title, int? ParentId)
    : IRequest<Result<CategoryDto, Error>>;
