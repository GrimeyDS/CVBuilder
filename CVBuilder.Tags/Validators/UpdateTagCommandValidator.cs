using CVBuilder.Tags.Constants;
using CVBuilder.Tags.Commands;
using FluentValidation;
using CVBuilder.Shared.Validators;

namespace CVBuilder.Tags.Validators;

internal class UpdateTagCommandValidator : AbstractEntityValidator<UpdateTagCommand>
{
    public UpdateTagCommandValidator()
    {
        RuleFor(e => e.TagId)
            .NotEmpty().WithMessage(ErrorMessages.IdRequired)
            .GreaterThan(0).WithMessage(ErrorMessages.IdInvalid);

        RuleFor(e => e.Title)
            .NotEmpty().WithMessage(ErrorMessages.TitleRequired)
            .MaximumLength(100).WithMessage(ErrorMessages.TitleMaxLength);
    }
}