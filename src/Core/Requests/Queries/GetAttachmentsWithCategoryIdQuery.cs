namespace AttachManagement.Core.Requests.Queries;

public record GetAttachmentsWithCategoryIdQuery(int CategoryId)
    : IRequest<Result<AttachmentDto[], Error>>;
