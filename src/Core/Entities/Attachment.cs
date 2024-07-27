namespace AttachManagement.Core.Entities;

public sealed class Attachment : IHaveId<Guid>
{
    private readonly Guid _id = Guid.Empty;

#pragma warning disable CS8618 // Required by Entity Framework
    private Attachment() { }
#pragma warning restore CS8618

    private Attachment(string fileName, FileType fileType, string? description)
    {
        if (fileType == FileType.Unknown)
        {
            throw new ArgumentException("File type is unknown.", nameof(fileType));
        }

        FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        FileType = fileType;
        Description = description;
        CreatedOnUtc = DateTimeOffset.UtcNow;
    }

    public static Attachment Create(string fileName, FileType fileType, string? description = null) =>
        new(fileName, fileType, description);

    public void ModifyFileInfo(string fileName, string? description)
    {
        FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        Description = description;
        LastModifiedOnUtc = DateTimeOffset.UtcNow;
    }

    public string FileName { get; private set; }

    public FileType FileType { get; private set; }

    public string? Description { get; private set; }

    public DateTimeOffset CreatedOnUtc { get; }

    public DateTimeOffset? LastModifiedOnUtc { get; private set; }

    Guid IHaveId<Guid>.Id => _id;
}
