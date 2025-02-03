using CVBuilder.Shared.Models;
using MediatR;

namespace CVBuilder.Shared.Queries;

public sealed record GetExperiencesByProfileIdsQuery(List<int> ProfileIds) : IRequest<GetExperiencesByProfileIdsQueryResponse>;

public sealed record GetExperiencesByProfileIdsQueryResponse(Dictionary<int, List<ExperienceModel>> Experiences);
