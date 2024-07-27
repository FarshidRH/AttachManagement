namespace AttachManagement.Infrastructure.Handlers.QueryHandlers;

public class GetAttachmentWithIdQueryHandler(AppDbContext dbContext)
    : IRequestHandler<GetAttachmentWithIdQuery, Result<AttachmentDto, Error>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Result<AttachmentDto, Error>> Handle(
        GetAttachmentWithIdQuery request, CancellationToken cancellationToken)
    {
        Attachment? attachment = await _dbContext.AttachmentWithIdAsync(request.Id, asNoTracking: true, cancellationToken);
        if (attachment == null)
        {
            return AttachmentErrors.AttachmentNotFound;
        }

        int categoryId = _dbContext.Entry(attachment).Property<int>("category_id").CurrentValue;
        return attachment.ToModel(categoryId);
    }
}
