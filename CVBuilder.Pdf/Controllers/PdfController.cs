using CVBuilder.Pdf.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Pdf.Controllers;

[ApiController]
[Route("[controller]")]
public class PdfController : ControllerBase
{
    private readonly IMediator _mediator;

    public PdfController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{profileId}/{templateId}")]
    public async Task<IActionResult> DownloadPDF(int profileId, int templateId)
    {
        var query = new GetPdfQuery(templateId, profileId);
        var response = await _mediator.Send(query);

        if (response == null || response.FileContent == null)
            return NotFound();

        return File(response.FileContent, "application/pdf", "CV.pdf");
    }

    [HttpGet]
    public async Task<IActionResult> GetTemplates()
    {
        var query = new GetTemplatesQuery();
        var response = await _mediator.Send(query);

        if (response == null || response.Templates == null)
            return NotFound();

        return Ok(response);
    }
}