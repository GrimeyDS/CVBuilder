using CVBuilder.Profiles.Commands;
using CVBuilder.Profiles.Constants;
using CVBuilder.Profiles.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Profiles.Handlers;

internal class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UpdateProfileCommandResponse>
{
    private readonly ProfileDbContext _context;

    public UpdateProfileCommandHandler(ProfileDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateProfileCommandResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var oldProfile = await _context.Profiles.FirstOrDefaultAsync(u => u.Id.Equals(request.ProfileId), cancellationToken);

        if (oldProfile is null)
            throw new Exception(ErrorMessages.ProfileNotFound);

        oldProfile.FirstName = request.FirstName.Trim();
        oldProfile.LastName = request.LastName.Trim();
        oldProfile.BirthDate = request.BirthDate;
        oldProfile.Description = request.Description;
        oldProfile.PictureUrl = request.PictureName ?? "profile-placeholder.jpg";
        oldProfile.CurrentRole = request.CurrentRole;
        oldProfile.IsFirstTimeSetup = false;

        _context.Profiles.Update(oldProfile);
        await _context.SaveChangesAsync(cancellationToken);
        return new UpdateProfileCommandResponse(oldProfile.Id);
    }

}
