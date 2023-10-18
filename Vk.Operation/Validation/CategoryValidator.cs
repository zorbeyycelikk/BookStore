using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateCategoryValidator: AbstractValidator<CategoryCreateRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.CategoryNumber).NotEmpty();
        RuleFor(x => x.CategoryName).NotEmpty().MaximumLength(50);;
    }
}

public class UpdateCategoryValidator: AbstractValidator<CategoryUpdateRequest>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.CategoryName).NotEmpty().MaximumLength(50);
    }
}