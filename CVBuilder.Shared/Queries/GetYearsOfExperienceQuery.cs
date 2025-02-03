using MediatR;

namespace CVBuilder.Shared.Queries;

public sealed record GetYearsOfExperienceQuery(List<int> ProfileIds) : IRequest<GetYearsOfExperienceQueryResponse>;

public sealed record GetYearsOfExperienceQueryResponse(Dictionary<int, int> YearsOfExperience);
