using CVBuilder.Shared.Models;
using MediatR;

namespace CVBuilder.Projects.Queries;

public sealed record GetProjectsQuery(int? Id = null) : IRequest<GetProjectsQueryResponse>;

public sealed record GetProjectsQueryResponse(IEnumerable<ProjectModel> Projects);