using CVBuilder.Projects.Constants;
using CVBuilder.Projects.Commands;
using FluentValidation;
using CVBuilder.Shared.Validators;

namespace CVBuilder.Projects.Validators;

internal class UpdateProjectCommandValidator : AbstractEntityValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(project => project.ProjectId)
            .NotEmpty().WithMessage(ErrorMessages.ProjectIdRequired)
            .GreaterThan(0).WithMessage(ErrorMessages.ProjectIdInvalid);

        RuleFor(project => project.Title)
            .NotEmpty().WithMessage(ErrorMessages.TitleRequired)
            .MaximumLength(50).WithMessage(ErrorMessages.TitleMaxLength);

        RuleFor(project => project.Customer)
            .NotEmpty().WithMessage(ErrorMessages.CustomerRequired)
            .MaximumLength(50).WithMessage(ErrorMessages.CustomerMaxLength);

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

        RuleFor(project => project.Description)
            .MaximumLength(500).WithMessage(ErrorMessages.DescriptionMaxLength);
    }
}
