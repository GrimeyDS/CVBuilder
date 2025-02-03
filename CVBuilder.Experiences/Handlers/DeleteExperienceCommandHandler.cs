using CVBuilder.Experiences.Commands;
using CVBuilder.Experiences.Constants;
using CVBuilder.Experiences.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Experiences.Handlers;

internal class DeleteExperienceCommandHandler : IRequestHandler<DeleteExperienceCommand, DeleteExperienceCommandResponse>
{
    private readonly ExperienceDbContext _context;

    public DeleteExperienceCommandHandler(ExperienceDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteExperienceCommandResponse> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Experiences.Where(e => e.Id == request.Id)
                                               .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
            throw new Exception(ErrorMessages.ExperienceNotFound);

        _context.Experiences.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteExperienceCommandResponse(entity.Id);   
    }
}