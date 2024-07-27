namespace AttachManagement.Core.Entities;

public sealed class Category
{
    private readonly List<Category> _subCategories = [];
    private readonly List<Attachment> _attachments = [];

#pragma warning disable CS8618 // Required by Entity Framework
    private Category() { }
#pragma warning restore CS8618

    private Category(string title) =>
        Title = title ?? throw new ArgumentNullException(nameof(title));

    public static Category Create(string title) => new(title);

    public Category AddSubCategory(string categoryTitle)
    {
        var newCategory = Category.Create(categoryTitle);
        _subCategories.Add(newCategory);

        return newCategory;
    }

    public Attachment AttachFile(string fileName, FileType fileType, string? description = null)
    {
        if (HasSubCategories())
        {
            throw new InvalidOperationException("The attachment cannot be added to a category that has children.");
        }

        var newAttach = Attachment.Create(fileName, fileType, description);
        _attachments.Add(newAttach);

        return newAttach;
    }

    public void Modify(string title)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    public bool HasSubCategories() => _subCategories.Any();

    public string Title { get; private set; }

    public IReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();

    public IReadOnlyCollection<Attachment> Attachments => _attachments.AsReadOnly();
}
