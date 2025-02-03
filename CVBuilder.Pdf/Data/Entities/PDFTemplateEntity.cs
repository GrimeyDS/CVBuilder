using CVBuilder.Shared.Data;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Pdf.Data.Entities;

internal class PDFTemplateEntity : EntityBase
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(250)]
    public string Address { get; set; } = string.Empty;
    [MaxLength(50)]
    public string PhoneNumber { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Email { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Website { get; set; } = string.Empty;
    [MaxLength(50)]
    public string SubTitle { get; set; } = string.Empty;
    [MaxLength(50)]
    public string PrimaryColor { get; set; } = string.Empty;
    [MaxLength(50)]
    public string SecondColor { get; set; } = string.Empty;
    [MaxLength(50)]
    public string ThirdColor { get; set; } = string.Empty;
    [MaxLength(250)]
    public string LogoUrl { get; set; } = string.Empty;
    [MaxLength(250)]
    public string FooterImageUrl { get; set; } = string.Empty;

}
