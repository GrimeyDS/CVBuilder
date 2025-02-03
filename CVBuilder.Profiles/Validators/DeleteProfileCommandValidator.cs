using CVBuilder.Profiles.Commands;
using CVBuilder.Profiles.Constants;
using CVBuilder.Shared.Validators;
using FluentValidation;

namespace CVBuilder.Profiles.Validators;

internal class DeleteProfileCommandValidator : AbstractEntityValidator<DeleteProfileCommand>
{
    public DeleteProfileCommandValidator()
    {
        RuleFor(profile => profile.ProfileId)
            .NotEmpty().WithMessage(ErrorMessages.ProfileIdRequired)
            .GreaterThan(0).WithMessage(ErrorMessages.ProfileIdInvalid);
    }
}
