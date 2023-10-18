using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateBookValidator: AbstractValidator<BookCreateRequest>
{
    public CreateBookValidator()
    {
        RuleFor(x => x.BookNumber).NotEmpty();
        RuleFor(x => x.HeadLine).NotEmpty();
        RuleFor(x => x.PageCount).NotEmpty();
        RuleFor(x => x.Publisher).MaximumLength(50);
        RuleFor(x => x.ISNB).NotEmpty().MaximumLength(13);
        RuleFor(x => x.AuthorId).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}

public class UpdateBookValidator: AbstractValidator<BookUpdateRequest>
{
    public UpdateBookValidator()
    {
        RuleFor(x => x.BookNumber).NotEmpty();
        RuleFor(x => x.PageCount).NotEmpty();
        RuleFor(x => x.Publisher).MaximumLength(50);
    }
}