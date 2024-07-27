namespace AttachManagement.Core.Requests.Queries;

public record GetAttachmentWithIdQuery(Guid Id)
    : IRequest<Result<AttachmentDto, Error>>;
