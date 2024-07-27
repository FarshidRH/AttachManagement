namespace AttachManagement.Infrastructure.Extensions;

internal static class AppDbContextExtension
{
    public static async Task<Category?> CategoryWithIdAsync(
        this AppDbContext dbContext, int id, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Category> query = dbContext.Set<Category>();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return await query.SingleOrDefaultAsync(x => EF.Property<int>(x, "id") == id, cancellationToken);
    }

    public static async Task<Attachment?> AttachmentWithIdAsync(
        this AppDbContext dbContext, Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Attachment> query = dbContext.Set<Attachment>();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return await query.SingleOrDefaultAsync(x => ((IHaveId<Guid>)x).Id == id, cancellationToken);
    }
}
