using CVBuilder.Projects.Commands;
using CVBuilder.Projects.Data;
using CVBuilder.Projects.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Profiles.Handlers;

internal class UpdateProjectTagsCommandHandler : IRequestHandler<UpdateProjectTagsCommand, UpdateProjectTagsCommandResponse>
{
    private readonly ProjectDbContext _context;

    public UpdateProjectTagsCommandHandler(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateProjectTagsCommandResponse> Handle(UpdateProjectTagsCommand request, CancellationToken cancellationToken)
    {
        var profileTags = await _context.ProjectTags.Where(pt => pt.ProjectId == request.Id).ToListAsync(cancellationToken);

        var tagsToAdd = request.TagIds.Except(profileTags.Select(pt => pt.TagId)).ToList();
        var tagsToRemove = profileTags.Select(pt => pt.TagId).Except(request.TagIds).ToList();

        foreach (var tagId in tagsToAdd)
        {
            var profileTag = new ProjectTagEntity
            {
                ProjectId = request.Id,
                TagId = tagId
            };
            await _context.ProjectTags.AddAsync(profileTag, cancellationToken);
        }

        foreach (var tagId in tagsToRemove)
        {
            var profileTag = profileTags.FirstOrDefault(pt => pt.TagId == tagId);
            if (profileTag != null)
                _context.ProjectTags.Remove(profileTag);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return new UpdateProjectTagsCommandResponse(request.Id);
    }

}
