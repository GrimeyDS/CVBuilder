using CVBuilder.Shared.Validators;
using CVBuilder.Users.Commands;
using CVBuilder.Users.Constants;
using FluentValidation;

namespace CVBuilder.Users.Validators;

internal class CreateUserCommandValidator : AbstractEntityValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(user => user.EntraId)
            .NotEmpty().WithMessage(ErrorMessages.EntraIdRequired);
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage(ErrorMessages.EmailRequired)
            .EmailAddress().WithMessage(ErrorMessages.EmailInvalid);
    }
}