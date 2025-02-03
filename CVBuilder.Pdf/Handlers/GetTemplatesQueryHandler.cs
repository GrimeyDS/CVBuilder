using CVBuilder.Pdf.Data;
using CVBuilder.Pdf.Queries;
using CVBuilder.Pdf.Mapping;
using MediatR;
using CVBuilder.Pdf.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Pdf.Handlers;

internal class GetTemplatesQueryHandler : IRequestHandler<GetTemplatesQuery, GetTemplatesQueryResponse>
{
    private readonly PDFTemplateDbContext _context;

    public GetTemplatesQueryHandler(PDFTemplateDbContext context)
    {
        _context = context;
    }

    public async Task<GetTemplatesQueryResponse> Handle(GetTemplatesQuery request, CancellationToken cancellationToken)
    {
        var result = new List<PDFTemplateEntity>();

        if (request.Id.HasValue)
        {
            var template = await _context.PDFTemplates.Where(e => e.Id == request.Id).FirstOrDefaultAsync();
            if (template != null) result.Add(template);
        }
        else
        {
            result = await _context.PDFTemplates.ToListAsync(cancellationToken: cancellationToken);
        }

        var response = new GetTemplatesQueryResponse(result.Select(u => u.ToTemplateModel()));
        return response;

    }
}
