using CVBuilder.Profiles.Commands;
using CVBuilder.Profiles.Constants;
using CVBuilder.Shared.Validators;
using FluentValidation;

namespace CVBuilder.Profiles.Validators;

internal class CreateProfileCommandValidator : AbstractEntityValidator<CreateProfileCommand>
{
    public CreateProfileCommandValidator()
    {
        RuleFor(profile => profile.UserId)
            .NotEmpty().WithMessage(ErrorMessages.UserIdRequired)
            .GreaterThan(0).WithMessage(ErrorMessages.UserIdInvalid);

        RuleFor(profile => profile.FirstName)
            .NotEmpty().WithMessage(ErrorMessages.FirstNameRequired)
            .MaximumLength(50).WithMessage(ErrorMessages.FirstNameMaxLength);

        RuleFor(profile => profile.LastName)
            .NotEmpty().WithMessage(ErrorMessages.LastNameRequired)
            .MaximumLength(50).WithMessage(ErrorMessages.LastNameMaxLength);

        RuleFor(profile => profile.BirthDate)
            .NotEmpty().WithMessage(ErrorMessages.BirthDateRequired)
            .LessThan(DateTime.Now).WithMessage(ErrorMessages.BirthDateInvalid);

        RuleFor(profile => profile.Description)
            .MaximumLength(500).WithMessage(ErrorMessages.DescriptionMaxLength);

        RuleFor(profile => profile.CurrentRole)
            .NotEmpty().WithMessage(ErrorMessages.CurrentRoleRequired)
            .MaximumLength(250).WithMessage(ErrorMessages.CurrentRoleMaxLength);
    }
}
