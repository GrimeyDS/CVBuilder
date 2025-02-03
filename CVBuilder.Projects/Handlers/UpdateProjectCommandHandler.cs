using CVBuilder.Projects.Constants;
using CVBuilder.Projects.Commands;
using CVBuilder.Projects.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Projects.Handlers;

internal class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, UpdateProjectCommandResponse>
{
    private readonly ProjectDbContext _context;

    public UpdateProjectCommandHandler(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateProjectCommandResponse> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var oldProject = await _context.Projects.FirstOrDefaultAsync(p => p.Id.Equals(request.ProjectId), cancellationToken);

        if (oldProject is null)
            throw new Exception(ErrorMessages.ProjectNotFound);

        oldProject.Title = request.Title.Trim();
        oldProject.Customer = request.Customer.Trim();
        oldProject.StartDate = request.StartDate;
        oldProject.EndDate = request.EndDate;
        oldProject.Description = request.Description;

        if (!string.IsNullOrWhiteSpace(request.PictureName))
            oldProject.PictureUrl = request.PictureName.Trim() ?? "project-placeholder.png";

        _context.Projects.Update(oldProject);
        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateProjectCommandResponse(oldProject.Id);
    }

}
