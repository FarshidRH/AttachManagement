using System.Globalization;

namespace AttachManagement.Infrastructure.Handlers.CommandHandlers;

public class AddAttachmentCommandHandler(AppDbContext dbContext)
    : IRequestHandler<AddAttachmentCommand, Result<AttachmentDto, Error>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Result<AttachmentDto, Error>> Handle(
        AddAttachmentCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _dbContext.CategoryWithIdAsync(request.CategoryId, cancellationToken: cancellationToken);
        if (category == null)
        {
            return CategoryErrors.CategoryNotFound;
        }

        try
        {
            // Save attachment info to database.
            Attachment newAttachment = category.AttachFile(request.FileName, request.FileType, request.Description);
            await _dbContext.AddAsync(newAttachment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Save attachment file to disk.
            const string attachmentDirectory = @"D:\AttachManagementFiles";
            string attachmentFileName = $"{((IHaveId<Guid>)newAttachment).Id}.{request.FileType}".ToLower(CultureInfo.InvariantCulture);
            string filePath = Path.Combine(attachmentDirectory, attachmentFileName);
            await File.WriteAllBytesAsync(filePath, request.FileData, cancellationToken);

            return newAttachment.ToModel(request.CategoryId);
        }
        catch (Exception exception)
        {
            return exception.ToError();
        }
    }
}
