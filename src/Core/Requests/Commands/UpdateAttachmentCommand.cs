namespace AttachManagement.Core.Requests.Commands;

public record UpdateAttachmentCommand(Guid Id, string FileName, string? Description)
    : IRequest<Result<AttachmentDto, Error>>;