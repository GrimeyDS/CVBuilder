using CVBuilder.Experiences.Constants;
using CVBuilder.Experiences.Commands;
using FluentValidation;
using CVBuilder.Shared.Validators;

namespace CVBuilder.Experiences.Validators;

internal class DeleteExperienceCommandValidator : AbstractEntityValidator<DeleteExperienceCommand>
{
    public DeleteExperienceCommandValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty().WithMessage(ErrorMessages.IdRequired)
            .GreaterThan(0).WithMessage(ErrorMessages.IdInvalid);
    }
}