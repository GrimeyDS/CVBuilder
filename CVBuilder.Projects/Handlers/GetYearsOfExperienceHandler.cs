using CVBuilder.Projects.Data;
using CVBuilder.Shared.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Profiles.Handlers;

internal class GetYearsOfExperienceHandler : IRequestHandler<GetYearsOfExperienceQuery, GetYearsOfExperienceQueryResponse>
{
    private readonly ProjectDbContext _context;

    public GetYearsOfExperienceHandler(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<GetYearsOfExperienceQueryResponse> Handle(GetYearsOfExperienceQuery request, CancellationToken cancellationToken)
    {
        var projects = await _context.Projects.Where(p => request.ProfileIds.Contains(p.ProfileId)).ToListAsync();
        var yearsOfExperience = projects.GroupBy(p => p.ProfileId).ToDictionary(g => g.Key, g => g.Sum(p => p.Duration));

        return new GetYearsOfExperienceQueryResponse(yearsOfExperience);
    }
}
