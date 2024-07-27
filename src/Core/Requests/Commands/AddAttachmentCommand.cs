namespace AttachManagement.Core.Requests.Commands;

public record AddAttachmentCommand(
    int CategoryId,
    string FileName,
    FileType FileType,
    byte[] FileData,
    string? Description) : IRequest<Result<AttachmentDto, Error>>;
