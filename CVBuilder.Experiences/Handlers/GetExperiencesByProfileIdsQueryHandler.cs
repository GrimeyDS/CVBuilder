using CVBuilder.Experiences.Data;
using CVBuilder.Experiences.Mapping;
using CVBuilder.Shared.Models;
using CVBuilder.Shared.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Experiences.Handlers;

internal class GetExperiencesByProfileIdsQueryHandler : IRequestHandler<GetExperiencesByProfileIdsQuery, GetExperiencesByProfileIdsQueryResponse>
{
    private readonly ExperienceDbContext _context;

    public GetExperiencesByProfileIdsQueryHandler(ExperienceDbContext context)
    {
        _context = context;
    }

    public async Task<GetExperiencesByProfileIdsQueryResponse> Handle(GetExperiencesByProfileIdsQuery request, CancellationToken cancellationToken)
    {
        var result = new Dictionary<int, List<ExperienceModel>>();
        var experiences = await _context.Experiences.ToListAsync(cancellationToken);

        foreach (int profileId in request.ProfileIds)
        {
            var profileExperiences = experiences.Where(e => e.ProfileId == profileId).ToList();
            var orderedExperiences = profileExperiences.OrderBy(e => e.EndDate != null)
                                                       .ThenByDescending(e => e.EndDate)
                                                       .ThenByDescending(e => e.StartDate);
            result.Add(profileId, orderedExperiences.Select(e => e.ToModel()).ToList());
        }

        return new GetExperiencesByProfileIdsQueryResponse(result);
    }
}