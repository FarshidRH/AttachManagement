namespace AttachManagement.Core.Errors;

public static class AttachmentErrors
{
    public static readonly Error AttachmentNotFound = Error.NotFound("Attachment.AttachmentNotFound", "Attachment not found");
}
