namespace AttachManagement.Infrastructure.Handlers.CommandHandlers;

public class DeleteAttachmentCommandHandler(AppDbContext dbContext)
    : IRequestHandler<DeleteAttachmentCommand, Result<bool, Error>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Result<bool, Error>> Handle(
        DeleteAttachmentCommand request, CancellationToken cancellationToken)
    {
        Attachment? attachment = await _dbContext.AttachmentWithIdAsync(request.Id, cancellationToken: cancellationToken);
        if (attachment == null)
        {
            return AttachmentErrors.AttachmentNotFound;
        }

        try
        {
            _dbContext.Remove(attachment);
            int dbResult = await _dbContext.SaveChangesAsync(cancellationToken);

            return dbResult > 0;
        }
        catch (Exception exception)
        {
            return exception.ToError();
        }
    }
}
