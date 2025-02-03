using CVBuilder.Experiences.Commands;
using CVBuilder.Experiences.Constants;
using CVBuilder.Experiences.Data;
using CVBuilder.Experiences.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Experiences.Handlers;

internal class CreateExperienceCommandHandler : IRequestHandler<CreateExperienceCommand, CreateExperienceCommandResponse>
{
    private readonly ExperienceDbContext _context;

    public CreateExperienceCommandHandler(ExperienceDbContext context)
    {
        _context = context;
    }

    public async Task<CreateExperienceCommandResponse> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
    {
        var existingExperience = await _context.Experiences.Where(e => e.ProfileId == request.ProfileId && e.Title == request.Title)
                                                           .FirstOrDefaultAsync(cancellationToken);

        if (existingExperience != null)
            throw new Exception(ErrorMessages.ExperienceAlreadyExists);

        var entity = new ExperienceEntity
        {
            ProfileId = request.ProfileId,
            Title = request.Title.Trim(),
            Description = request.Description,
            Type = request.Type.ToString(),
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        _context.Experiences.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateExperienceCommandResponse(entity.Id);
    }

}