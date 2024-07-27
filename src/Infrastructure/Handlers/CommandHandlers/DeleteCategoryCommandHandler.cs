namespace AttachManagement.Infrastructure.Handlers.CommandHandlers;

public class DeleteCategoryCommandHandler(AppDbContext dbContext)
    : IRequestHandler<DeleteCategoryCommand, Result<bool, Error>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Result<bool, Error>> Handle(
        DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryProjection = await _dbContext.Set<Category>()
            .Where(x => EF.Property<int>(x, "id") == request.Id)
            .Select(x => new
            {
                Category = x,
                HasSubCategories = x.HasSubCategories(),
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (categoryProjection == null)
        {
            return CategoryErrors.CategoryNotFound;
        }

        if (categoryProjection.HasSubCategories)
        {
            return CategoryErrors.CategoryDeletionImpossible;
        }

        try
        {
            _dbContext.Remove(categoryProjection.Category);
            int dbResult = await _dbContext.SaveChangesAsync(cancellationToken);

            return dbResult > 0;
        }
        catch (Exception exception)
        {
            return exception.ToError();
        }
    }
}
