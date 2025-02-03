using CVBuilder.Shared.Models;
using MediatR;

namespace CVBuilder.Shared.Queries;

public sealed record GetProjectsByProfileIdsQuery(List<int> ProfileIds) : IRequest<GetProjectsByProfileIdsQueryResponse>;

public sealed record GetProjectsByProfileIdsQueryResponse(Dictionary<int, List<ProjectModel>> Projects);
