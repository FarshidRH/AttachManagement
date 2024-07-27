namespace AttachManagement.Core.Extensions;

public static class DtoExtensions
{
    public static CategoryDto ToModel(this Category category, int categoryId, int? parentCategoryId)
    {
        return new(categoryId, category.Title, parentCategoryId);
    }

    public static AttachmentDto ToModel(this Attachment attachment, int categoryId)
    {
        return new(((IHaveId<Guid>)attachment).Id, attachment.FileName,
            attachment.FileType, attachment.Description, categoryId,
            attachment.CreatedOnUtc.LocalDateTime, attachment.LastModifiedOnUtc?.LocalDateTime);
    }
}
