using CVBuilder.Pdf.Models;
using MediatR;

namespace CVBuilder.Pdf.Queries;

public sealed record GetTemplatesQuery(int? Id = null) : IRequest<GetTemplatesQueryResponse>;

public sealed record GetTemplatesQueryResponse(IEnumerable<TemplateModel> Templates);
