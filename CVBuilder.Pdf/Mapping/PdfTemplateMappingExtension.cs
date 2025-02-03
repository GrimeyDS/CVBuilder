using CVBuilder.Pdf.Data.Entities;
using CVBuilder.Pdf.Models;

namespace CVBuilder.Pdf.Mapping;

internal static class TagMappingExtensions
{
    public static PDFTemplateModel ToModel(this PDFTemplateEntity entity)
    {
        return new PDFTemplateModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Address = entity.Address,
            PhoneNumber = entity.PhoneNumber,
            Email = entity.Email,
            Website = entity.Website,
            SubTitle = entity.SubTitle,
            PrimaryColor = entity.PrimaryColor,
            SecondColor = entity.SecondColor,
            ThirdColor = entity.ThirdColor,
            LogoUrl = entity.LogoUrl,
            FooterImageUrl = entity.FooterImageUrl
        };
    }

    public static TemplateModel ToTemplateModel(this PDFTemplateEntity entity)
    {
        return new TemplateModel
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}