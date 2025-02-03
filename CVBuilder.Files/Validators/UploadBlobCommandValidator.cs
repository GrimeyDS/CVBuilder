using CVBuilder.Files.Commands;
using CVBuilder.Files.Constants;
using CVBuilder.Shared.Validators;
using FluentValidation;

namespace CVBuilder.Files.Validators;

internal class UploadBlobCommandValidator : AbstractEntityValidator<UploadBlobCommand>
{
    public UploadBlobCommandValidator()
    {
        RuleFor(x => x.File.Length).NotNull().LessThanOrEqualTo(5000000).WithMessage(FileErrorMessages.FileSizeError);
        RuleFor(x => x.File.FileName).Custom((fileName, context) =>
        {
            if (fileName != null)
            {
                string extension = Path.GetExtension(fileName).ToLower();

                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                    context.AddFailure(FileErrorMessages.FileTypeError);
            }
        });
    }

}
