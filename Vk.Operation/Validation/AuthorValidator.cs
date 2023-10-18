using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateAuthorValidator: AbstractValidator<AuthorCreateRequest>
{
    public CreateAuthorValidator()
    {
        RuleFor(x => x.AuthorNumber).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Surname).NotEmpty().MaximumLength(20);
    }
}

public class UpdateAuthorValidator: AbstractValidator<AuthorUpdateRequest>
{
    public UpdateAuthorValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Surname).NotEmpty().MaximumLength(20);    }
}