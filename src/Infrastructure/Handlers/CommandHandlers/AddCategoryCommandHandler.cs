namespace AttachManagement.Infrastructure.Handlers.CommandHandlers;

public class AddCategoryCommandHandler(AppDbContext dbContext)
    : IRequestHandler<AddCategoryCommand, Result<CategoryDto, Error>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Result<CategoryDto, Error>> Handle(
        AddCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? parentCategory = null;
        int? parentCategoryId = request.ParentId;

        if (parentCategoryId.HasValue)
        {
            parentCategory = await _dbContext.CategoryWithIdAsync(parentCategoryId.Value, cancellationToken: cancellationToken);

            if (parentCategory == null)
            {
                return CategoryErrors.CategoryNotFound;
            }
        }

        try
        {
            Category newCategory;
            string newCategoryTitle = request.Title;

            if (parentCategory != null)
            {
                newCategory = parentCategory.AddSubCategory(newCategoryTitle);
            }
            else
            {
                newCategory = Category.Create(newCategoryTitle);
                await _dbContext.AddAsync(newCategory, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            int newCategoryId = _dbContext.Entry(newCategory).Property<int>("id").CurrentValue;
            return newCategory.ToModel(newCategoryId, parentCategoryId);
        }
        catch (Exception exception)
        {
            return exception.ToError();
        }
    }
}
