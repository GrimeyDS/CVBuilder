namespace CVBuilder.Pdf.Models;

public class PDFTemplateModel : TemplateModel 
{
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    public string PrimaryColor { get; set; } = string.Empty;
    public string SecondColor { get; set; } = string.Empty;
    public string ThirdColor { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string FooterImageUrl { get; set; } = string.Empty;
}
