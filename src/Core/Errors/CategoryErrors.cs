namespace AttachManagement.Core.Errors;

public static class CategoryErrors
{
    public static readonly Error CategoryNotFound = Error.NotFound("Category.CategoryNotFound", "Category not found.");

    public static readonly Error CategoryDeletionImpossible = Error.Failure("Category.CategoryDeletionImpossible", "It is impossible to delete the category.");
}
