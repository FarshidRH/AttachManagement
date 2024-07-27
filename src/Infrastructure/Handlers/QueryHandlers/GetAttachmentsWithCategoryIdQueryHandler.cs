namespace AttachManagement.Infrastructure.Handlers.QueryHandlers;

public class GetAttachmentsWithCategoryIdQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetAttachmentsWithCategoryIdQuery, Result<AttachmentDto[], Error>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Result<AttachmentDto[], Error>> Handle(
        GetAttachmentsWithCategoryIdQuery request, CancellationToken cancellationToken)
    {
        int categoryId = request.CategoryId;

        Category? category = await _dbContext.Set<Category>()
            .AsNoTracking()
            .Include("_attachments")
            .Where(x => EF.Property<int>(x, "id") == categoryId)
            .SingleOrDefaultAsync(cancellationToken);

        return category == null
            ? CategoryErrors.CategoryNotFound
            : category.Attachments.Select(x => x.ToModel(categoryId)).ToArray();
    }
}
