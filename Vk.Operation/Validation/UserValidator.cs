using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateUserValidator: AbstractValidator<UserCreateRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.UserNumber).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Role).NotEmpty().MaximumLength(10);
    }
}

public class UpdateUserValidator: AbstractValidator<UserUpdateRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
    }
}