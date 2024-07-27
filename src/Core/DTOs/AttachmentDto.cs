namespace AttachManagement.Core.DTOs;

public record AttachmentDto(
    Guid Id,
    string FileName,
    FileType FileType,
    string? Description,
    int CategoryId,
    DateTime CreatedOn,
    DateTime? LastModifiedOn);