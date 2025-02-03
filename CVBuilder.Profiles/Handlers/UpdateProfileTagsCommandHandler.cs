using CVBuilder.Profiles.Commands;
using CVBuilder.Profiles.Constants;
using CVBuilder.Profiles.Data;
using CVBuilder.Profiles.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Profiles.Handlers;

internal class UpdateProfileTagsCommandHandler : IRequestHandler<UpdateProfileTagsCommand, UpdateProfileTagsCommandResponse>
{
    private readonly ProfileDbContext _context;

    public UpdateProfileTagsCommandHandler(ProfileDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateProfileTagsCommandResponse> Handle(UpdateProfileTagsCommand request, CancellationToken cancellationToken)
    {
        var profileTags = await _context.ProfileTags.Where(pt => pt.ProfileId == request.Id).ToListAsync(cancellationToken);

        var tagsToAdd = request.TagIds.Except(profileTags.Select(pt => pt.TagId)).ToList();
        var tagsToRemove = profileTags.Select(pt => pt.TagId).Except(request.TagIds).ToList();

        foreach (var tagId in tagsToAdd)
        {
            var profileTag = new ProfileTagEntity
            {
                ProfileId = request.Id,
                TagId = tagId
            };
            await _context.ProfileTags.AddAsync(profileTag, cancellationToken);
        }

        foreach (var tagId in tagsToRemove)
        {
            var profileTag = profileTags.FirstOrDefault(pt => pt.TagId == tagId);
            if (profileTag != null)
                _context.ProfileTags.Remove(profileTag);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return new UpdateProfileTagsCommandResponse(request.Id);
    }

}
