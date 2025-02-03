using CVBuilder.Experiences.Constants;
using CVBuilder.Experiences.Commands;
using FluentValidation;
using CVBuilder.Shared.Validators;

namespace CVBuilder.Experiences.Validators;

internal class UpdateExperienceCommandValidator : AbstractEntityValidator<UpdateExperienceCommand>
{
    public UpdateExperienceCommandValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty().WithMessage(ErrorMessages.IdRequired)
            .GreaterThan(0).WithMessage(ErrorMessages.IdInvalid);

        RuleFor(e => e.ProfileId)
            .NotEmpty().WithMessage(ErrorMessages.ProfileIdRequired)
            .GreaterThan(0).WithMessage(ErrorMessages.ProfileIdInvalid);

        RuleFor(e => e.Title)
            .NotEmpty().WithMessage(ErrorMessages.TitleRequired)
            .MaximumLength(100).WithMessage(ErrorMessages.TitleMaxLength);

        RuleFor(project => project.StartDate)
            .NotEmpty().WithMessage(ErrorMessages.StartDateRequired)
            .LessThan(DateTime.Now).WithMessage(ErrorMessages.StartDateInvalid);

        RuleFor(project => project.StartDate)
             .LessThan(profile => profile.EndDate).WithMessage(ErrorMessages.StartDateInvalid)
             .When(project => project.EndDate.HasValue);

        RuleFor(project => project.EndDate)
            .LessThan(DateTime.Now).WithMessage(ErrorMessages.EndDateInvalid)
            .GreaterThan(profile => profile.StartDate).WithMessage(ErrorMessages.EndDateInvalid)
            .When(project => project.EndDate.HasValue);

        RuleFor(e => e.Description)
            .MaximumLength(500).WithMessage(ErrorMessages.DescriptionMaxLength);
    }
}