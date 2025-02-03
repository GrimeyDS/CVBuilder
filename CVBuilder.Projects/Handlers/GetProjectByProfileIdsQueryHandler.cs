using CVBuilder.Projects.Data;
using CVBuilder.Projects.Queries;
using CVBuilder.Shared.Models;
using CVBuilder.Shared.Queries;
using MediatR;

namespace CVBuilder.Projects.Handlers;

internal class GetProjectByProfileIdsQueryHandler : IRequestHandler<GetProjectsByProfileIdsQuery, GetProjectsByProfileIdsQueryResponse>
{
    private readonly ProjectDbContext _context;
    private readonly IMediator _mediator;

    public GetProjectByProfileIdsQueryHandler(ProjectDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<GetProjectsByProfileIdsQueryResponse> Handle(GetProjectsByProfileIdsQuery request, CancellationToken cancellationToken)
    {
        var result = new Dictionary<int, List<ProjectModel>>();

        var projectQuery = new GetProjectsQuery();
        var projectQueryResponse = await _mediator.Send(projectQuery);

        foreach (int profileId in request.ProfileIds)
        {
            var profileProjects = projectQueryResponse.Projects.Where(p => p.ProfileId == profileId).ToList();
            result.Add(profileId, profileProjects);
        }

        return new GetProjectsByProfileIdsQueryResponse(result);
    }
}