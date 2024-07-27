namespace AttachManagement.Infrastructure.Handlers.QueryHandlers;

public class GetCategoryWithIdQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetCategoryWithIdQuery, Result<CategoryDto, Error>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Result<CategoryDto, Error>> Handle(
        GetCategoryWithIdQuery request, CancellationToken cancellationToken)
    {
        int categoryId = request.Id;
        Category? category = await _dbContext.CategoryWithIdAsync(categoryId, asNoTracking: true, cancellationToken);

        if (category == null)
        {
            return CategoryErrors.CategoryNotFound;
        }

        int? parentId = _dbContext.Entry(category).Property<int?>("parent_id").CurrentValue;
        return category.ToModel(categoryId, parentId);
    }
}
