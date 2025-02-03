using CVBuilder.Shared.Models;
using MediatR;

namespace CVBuilder.Experiences.Queries;

public sealed record GetExperienceQuery(int? Id = null) : IRequest<GetExperienceQueryResponse>;

public sealed record GetExperienceQueryResponse(IEnumerable<ExperienceModel> Experiences);