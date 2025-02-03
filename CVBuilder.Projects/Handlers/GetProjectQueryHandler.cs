using CVBuilder.Projects.Data;
using CVBuilder.Projects.Data.Entities;
using CVBuilder.Projects.Mapping;
using CVBuilder.Shared.Models;
using CVBuilder.Projects.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CVBuilder.Shared.Queries;

namespace CVBuilder.Profiles.Handlers;

internal class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, GetProjectsQueryResponse>
{
    private readonly ProjectDbContext _context;
    private readonly IMediator _mediator;

    public GetProjectsQueryHandler(ProjectDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<GetProjectsQueryResponse> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var result = new List<ProjectEntity>();

        if (request.Id.HasValue)
        {
            var profile = await _context.Projects.FindAsync(request.Id);
            if (profile != null) result.Add(profile);
        }
        else
        {
            result = await _context.Projects.ToListAsync(cancellationToken: cancellationToken);
        }

        var modelsResult = result.OrderBy(p => p.EndDate != null)
                                 .ThenByDescending(p => p.EndDate)
                                 .ThenByDescending(p => p.StartDate)
                                 .Select(p => p.ToModel()).ToList();
        await GetTags(modelsResult);

        return new GetProjectsQueryResponse(modelsResult);
    }

    private async Task GetTags(List<ProjectModel> model)
    {
        var projectTagIds = await _context.ProjectTags.ToListAsync();
        Dictionary<int, List<int>> profileTags = projectTagIds.GroupBy(pt => pt.ProjectId)
                                                              .ToDictionary(g => g.Key, g => g.Select(pt => pt.TagId).ToList());

        var tagsQuery = new GetTagsByIdsQuery(profileTags);
        var tagsResponse = await _mediator.Send(tagsQuery);

        foreach (var project in model)
            project.Tags = tagsResponse.Tags.FirstOrDefault(t => t.Key == project.Id).Value;
    }

}
