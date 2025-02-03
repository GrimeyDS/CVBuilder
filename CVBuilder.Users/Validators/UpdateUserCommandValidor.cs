using CVBuilder.Shared.Validators;
using CVBuilder.Users.Commands;
using CVBuilder.Users.Constants;
using FluentValidation;

namespace CVBuilder.Users.Validators;

internal class UpdateUserCommandValidator : AbstractEntityValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(user => user.Id)
            .NotEmpty().WithMessage(ErrorMessages.IdRequired)
            .GreaterThan(0).WithMessage(ErrorMessages.IdInvalid);
        RuleFor(user => user.EntraId)
            .NotEmpty().WithMessage(ErrorMessages.EntraIdRequired);
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage(ErrorMessages.EmailRequired)
            .EmailAddress().WithMessage(ErrorMessages.EmailInvalid);
    }
}
