using CVBuilder.Experiences.Data;
using CVBuilder.Experiences.Data.Entities;
using CVBuilder.Experiences.Mapping;
using CVBuilder.Experiences.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Experiences.Handlers;

internal class GetExperienceQueryHandler : IRequestHandler<GetExperienceQuery, GetExperienceQueryResponse>
{
    private readonly ExperienceDbContext _context;

    public GetExperienceQueryHandler(ExperienceDbContext context)
    {
        _context = context;
    }

    public async Task<GetExperienceQueryResponse> Handle(GetExperienceQuery request, CancellationToken cancellationToken)
    {
        var result = new List<ExperienceEntity>();

        if (request.Id.HasValue)
        {
            var experience = await _context.Experiences.Where(e => e.Id == request.Id).FirstOrDefaultAsync();
            if (experience != null) result.Add(experience);
        }
        else
        {
            result = await _context.Experiences.ToListAsync(cancellationToken: cancellationToken);

        }

        var modelResult = result.Select(e => e.ToModel()).ToList();

        var response = new GetExperienceQueryResponse(modelResult);

        return response;
    }
}