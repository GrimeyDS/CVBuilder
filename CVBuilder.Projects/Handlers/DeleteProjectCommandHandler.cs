using CVBuilder.Projects.Constants;
using CVBuilder.Projects.Commands;
using CVBuilder.Projects.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Projects.Handlers;

internal class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, DeleteProjectCommandResponse>
{
    private readonly ProjectDbContext _context;

    public DeleteProjectCommandHandler(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteProjectCommandResponse> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);
        if (entity == null)
            throw new Exception(ErrorMessages.ProjectNotFound);

        _context.Projects.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteProjectCommandResponse(entity.Id, entity.PictureUrl);
    }

}