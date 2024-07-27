namespace AttachManagement.Core.Requests.Commands;

public record DeleteAttachmentCommand(Guid Id)
    : IRequest<Result<bool, Error>>;