namespace AttachManagement.Core.Requests.Queries;

public record GetAllCategoriesQuery()
    : IRequest<Result<CategoryDto[], Error>>;
