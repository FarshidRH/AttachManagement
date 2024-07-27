namespace AttachManagement.Infrastructure.Handlers.CommandHandlers;

public class UpdateCategoryCommandHandler(AppDbContext dbContext)
    : IRequestHandler<UpdateCategoryCommand, Result<CategoryDto, Error>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Result<CategoryDto, Error>> Handle(
        UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        int categoryId = request.Id;
        Category? category = await _dbContext.CategoryWithIdAsync(categoryId, cancellationToken: cancellationToken);

        if (category == null)
        {
            return CategoryErrors.CategoryNotFound;
        }

        try
        {
            category.Modify(request.Title);
            await _dbContext.SaveChangesAsync(cancellationToken);

            int? parentId = _dbContext.Entry(category).Property<int?>("parent_id").CurrentValue;
            return category.ToModel(categoryId, parentId);
        }
        catch (Exception exception)
        {
            return exception.ToError();
        }
    }
}
