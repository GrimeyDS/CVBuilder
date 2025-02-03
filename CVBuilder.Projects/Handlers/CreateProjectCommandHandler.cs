using CVBuilder.Projects.Constants;
using CVBuilder.Projects.Commands;
using CVBuilder.Projects.Data;
using CVBuilder.Projects.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Projects.Handlers;

internal class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CreateProjectCommandResponse>
{
    private readonly ProjectDbContext _context;

    public CreateProjectCommandHandler(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<CreateProjectCommandResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var existingProject = await _context.Projects.FirstOrDefaultAsync(p => p.ProfileId == request.ProfileId && p.Title == request.Title, cancellationToken);
        if (existingProject != null)
            throw new Exception(ErrorMessages.ProjectAlreadyExists);

        var entity = new ProjectEntity
        {
            ProfileId = request.ProfileId,
            Title = request.Title.Trim(),
            Customer = request.Customer.Trim(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Description = request.Description,
            PictureUrl = request.PictureName ?? "project-placeholder.png"
        };

        _context.Projects.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateProjectCommandResponse(entity.Id);
    }

}
