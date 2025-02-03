using CVBuilder.Profiles.Data;
using CVBuilder.Profiles.Data.Entities;
using CVBuilder.Profiles.Mapping;
using CVBuilder.Shared.Models;
using CVBuilder.Shared.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Profiles.Handlers;

internal class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, GetProfileQueryResponse>
{
    private readonly ProfileDbContext _context;
    private readonly IMediator _mediator;

    public GetProfileQueryHandler(ProfileDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<GetProfileQueryResponse> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var result = new List<ProfileEntity>();
        var modelResult = new List<ProfileModel>();

        if (request.Id.HasValue)
        {
            var profile = await _context.Profiles.FindAsync(request.Id);
            if (profile != null) result.Add(profile);

            modelResult = result.Select(p => p.ToModel()).ToList();

            await GetExperiences(modelResult);
            await GetProjects(modelResult);
        }
        else
        {
            result = await _context.Profiles.ToListAsync(cancellationToken: cancellationToken);
            modelResult = result.Select(p => p.ToModel()).ToList();
        }

        await GetYearsOfExperience(modelResult);
        await GetTags(modelResult);

        return new GetProfileQueryResponse(modelResult);
    }

    private async Task GetTags(List<ProfileModel> model)
    {
        var profileTagIds = await _context.ProfileTags.ToListAsync();
        Dictionary<int, List<int>> profileTags = profileTagIds.GroupBy(pt => pt.ProfileId)
                                                              .ToDictionary(g => g.Key, g => g.Select(pt => pt.TagId).ToList());

        var tagsQuery = new GetTagsByIdsQuery(profileTags);
        var tagsResponse = await _mediator.Send(tagsQuery);

        foreach (var profile in model)
            profile.Tags = tagsResponse.Tags.FirstOrDefault(t => t.Key == profile.Id).Value;
    }

    private async Task GetYearsOfExperience(List<ProfileModel> model)
    {
        var profileIds = model.Select(p => p.Id).ToList();
        var yearsOfExperienceQuery = new GetYearsOfExperienceQuery(profileIds);
        var yearsOfExperienceResponse = await _mediator.Send(yearsOfExperienceQuery);

        foreach (var profile in model)
            profile.YearsOfExperience = yearsOfExperienceResponse.YearsOfExperience.FirstOrDefault(y => y.Key == profile.Id).Value;
    }

    private async Task GetExperiences(List<ProfileModel> model)
    {
        var profileIds = model.Select(p => p.Id).ToList();

        var experiencesQuery = new GetExperiencesByProfileIdsQuery(profileIds);
        var experiencesResponse = await _mediator.Send(experiencesQuery);

        foreach (var profile in model)
            profile.Experiences = experiencesResponse.Experiences.FirstOrDefault(e => e.Key == profile.Id).Value;
    }

    private async Task GetProjects(List<ProfileModel> model)
    {
        var profileIds = model.Select(p => p.Id).ToList();

        var projectsQuery = new GetProjectsByProfileIdsQuery(profileIds);
        var projectsResponse = await _mediator.Send(projectsQuery);

        foreach (var profile in model)
            profile.Projects = projectsResponse.Projects.FirstOrDefault(p => p.Key == profile.Id).Value;
    }
}
