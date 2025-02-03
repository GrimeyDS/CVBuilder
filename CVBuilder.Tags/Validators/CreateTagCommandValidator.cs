using CVBuilder.Tags.Constants;
using CVBuilder.Tags.Commands;
using FluentValidation;
using CVBuilder.Shared.Validators;

namespace CVBuilder.Tags.Validators;

internal class CreateTagCommandValidator : AbstractEntityValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(e => e.Title)
            .NotEmpty().WithMessage(ErrorMessages.TitleRequired)
            .MaximumLength(100).WithMessage(ErrorMessages.TitleMaxLength);
    }
}