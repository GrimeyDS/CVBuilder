namespace CVBuilder.Pdf.Requests;

public sealed class DownloadPDFRequest(int TemplateId)
{
    public int TemplateId { get; set; } = TemplateId;
}