namespace AttachManagement.Infrastructure.Handlers.QueryHandlers;

public class GetAllCategoriesQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetAllCategoriesQuery, Result<CategoryDto[], Error>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Result<CategoryDto[], Error>> Handle(
        GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var allCategories = await _dbContext.Set<Category>()
            .AsNoTracking()
            .Select(x => new
            {
                Id = EF.Property<int>(x, "id"),
                x.Title,
                ParentId = EF.Property<int?>(x, "parent_id"),
            })
            .ToListAsync(cancellationToken);

        return allCategories.Select(x => new CategoryDto(x.Id, x.Title, x.ParentId)).ToArray();
    }
}
