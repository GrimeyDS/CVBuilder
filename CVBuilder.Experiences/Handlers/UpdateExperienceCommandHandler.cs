using CVBuilder.Experiences.Constants;
using CVBuilder.Experiences.Commands;
using CVBuilder.Experiences.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Experiences.Handlers;

internal class UpdateExperienceCommandHandler : IRequestHandler<UpdateExperienceCommand, UpdateExperienceCommandResponse>
{
    private readonly ExperienceDbContext _context;

    public UpdateExperienceCommandHandler(ExperienceDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateExperienceCommandResponse> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
    {
        var oldExperience = await _context.Experiences.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (oldExperience is null)
            throw new Exception(ErrorMessages.ExperienceNotFound);

        if (oldExperience.ProfileId != request.ProfileId)
            throw new Exception(ErrorMessages.ProfileExperienceNotFound);

        oldExperience.Title = request.Title;
        oldExperience.Description = request.Description;
        oldExperience.StartDate = request.StartDate;
        oldExperience.EndDate = request.EndDate;

        _context.Experiences.Update(oldExperience);
        await _context.SaveChangesAsync(cancellationToken);
        return new UpdateExperienceCommandResponse(oldExperience.Id);
    }

}