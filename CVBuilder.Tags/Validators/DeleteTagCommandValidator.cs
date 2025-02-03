using CVBuilder.Tags.Constants;
using CVBuilder.Tags.Commands;
using FluentValidation;
using CVBuilder.Shared.Validators;

namespace CVBuilder.Tags.Validators;

internal class DeleteTagCommandValidator : AbstractEntityValidator<DeleteTagCommand>
{
    public DeleteTagCommandValidator()
    {
        RuleFor(t => t.TagId)
            .NotEmpty().WithMessage(ErrorMessages.IdRequired)
            .GreaterThan(0).WithMessage(ErrorMessages.IdInvalid);

        RuleFor(t => t.OriginId)
            .NotEmpty().WithMessage(ErrorMessages.OriginIdRequired)
            .GreaterThan(0).WithMessage(ErrorMessages.OriginIdInvalid);
    }
}