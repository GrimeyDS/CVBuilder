using CVBuilder.Pdf.Constants;
using MediatR;

namespace CVBuilder.Pdf.Queries;

public sealed record GetPdfQuery(int TemplateId, int? ProfileId = null) : IRequest<GetPdfQueryResponse>;

public sealed record GetPdfQueryResponse(byte[] FileContent);
