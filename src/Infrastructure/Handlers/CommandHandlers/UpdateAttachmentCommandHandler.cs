namespace AttachManagement.Infrastructure.Handlers.CommandHandlers;

public class UpdateAttachmentCommandHandler(AppDbContext dbContext)
    : IRequestHandler<UpdateAttachmentCommand, Result<AttachmentDto, Error>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Result<AttachmentDto, Error>> Handle(
        UpdateAttachmentCommand request, CancellationToken cancellationToken)
    {
        Attachment? attachment = await _dbContext.AttachmentWithIdAsync(request.Id, cancellationToken: cancellationToken);
        if (attachment == null)
        {
            return AttachmentErrors.AttachmentNotFound;
        }

        try
        {
            attachment.ModifyFileInfo(request.FileName, request.Description);
            await _dbContext.SaveChangesAsync(cancellationToken);

            int categoryId = _dbContext.Entry(attachment).Property<int>("category_id").CurrentValue;
            return attachment.ToModel(categoryId);
        }
        catch (Exception exception)
        {
            return exception.ToError();
        }
    }
}
