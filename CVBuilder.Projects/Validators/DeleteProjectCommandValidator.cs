using CVBuilder.Projects.Commands;
using CVBuilder.Projects.Constants;
using CVBuilder.Shared.Validators;
using FluentValidation;

namespace CVBuilder.Projects.Validators;

internal class DeleteProjectCommandValidator : AbstractEntityValidator<DeleteProjectCommand>
{
    public DeleteProjectCommandValidator()
    {
        RuleFor(project => project.ProjectId)
            .NotEmpty().WithMessage(ErrorMessages.ProjectIdRequired)
            .GreaterThan(0).WithMessage(ErrorMessages.ProjectIdInvalid);
    }
}
