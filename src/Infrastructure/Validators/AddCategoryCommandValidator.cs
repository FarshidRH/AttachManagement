namespace AttachManagement.Infrastructure.Validators;

public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
{
    public AddCategoryCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(128);
    }
}
