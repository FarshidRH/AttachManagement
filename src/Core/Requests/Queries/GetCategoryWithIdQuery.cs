namespace AttachManagement.Core.Requests.Queries;

public record GetCategoryWithIdQuery(int Id)
    : IRequest<Result<CategoryDto, Error>>;
