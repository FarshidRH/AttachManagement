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

    public void AddSubCategory(Category category)
    {
        if (category == this)
        {
            throw new InvalidOperationException("The parent category and the child category are the same.");
        }

        if (_subCategories.Contains(category))
        {
            throw new InvalidOperationException("This category already exists in the children of the parent category.");
        }

        _subCategories.Add(category);
    }

    public void AttachFile(string fileName, string? description)
    {
        if (HasSubCategories())
        {
            throw new InvalidOperationException("The attachment cannot be added to a category that has children.");
        }

        var newAttach = Attachment.Create(fileName, description);
        _attachments.Add(newAttach);
    }

    public bool HasSubCategories() => _subCategories.Count != 0;

    public string Title { get; }

    public IReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();

    public IReadOnlyCollection<Attachment> Attachments => _attachments.AsReadOnly();
}
