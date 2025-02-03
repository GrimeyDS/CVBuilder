using CVBuilder.Pdf.Constants;
using CVBuilder.Pdf.Data;
using CVBuilder.Pdf.Queries;
using CVBuilder.Shared.Queries;
using CVBuilder.Pdf.Mapping;
using MediatR;

namespace CVBuilder.Pdf.Handlers;

internal class GetPdfQueryHandler : IRequestHandler<GetPdfQuery, GetPdfQueryResponse>
{
    private readonly IMediator _mediator;
    private readonly PDFTemplateDbContext _context;

    public GetPdfQueryHandler(IMediator mediator, PDFTemplateDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    public async Task<GetPdfQueryResponse> Handle(GetPdfQuery request, CancellationToken cancellationToken)
    {
        var profileQuery = new GetProfileQuery(request.ProfileId);
        var profileResponse = await _mediator.Send(profileQuery);
        var profile = profileResponse.Profiles.FirstOrDefault();

        if (profile == null)
            throw new Exception(ErrorMessages.ProfileNotFound);

        var template = _context.PDFTemplates.FirstOrDefault(t => t.Id == request.TemplateId);

        if (template == null)
            throw new Exception(ErrorMessages.TemplateNotFound);

        var templateModel = template.ToModel();

        var pdfService = new PDFService(profile, templateModel, _mediator);
        var pdfBytes = await pdfService.CreatePdf();

        return new GetPdfQueryResponse(pdfBytes);
    }
}
