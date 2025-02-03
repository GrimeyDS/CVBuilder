using CVBuilder.Profiles.Commands;
using CVBuilder.Profiles.Constants;
using CVBuilder.Profiles.Data;
using CVBuilder.Profiles.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Profiles.Handlers;

internal class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, CreateProfileCommandResponse>
{
    private readonly ProfileDbContext _context;

    public CreateProfileCommandHandler(ProfileDbContext context)
    {
        _context = context;
    }

    public async Task<CreateProfileCommandResponse> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        // uncomment when authentication is implemented
        // var existingProfile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == request.UserId, cancellationToken);
        // if (existingProfile != null)
        //    throw new Exception(ErrorMessages.ProfileAlreadyExists);

        var entity = new ProfileEntity
        {
            UserId = request.UserId,
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            BirthDate = request.BirthDate,
            Description = request.Description,
            PictureUrl = request.PictureName ?? "profile-placeholder.jpg",
            CurrentRole = request.CurrentRole,
            IsFirstTimeSetup = true
        };

        _context.Profiles.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateProfileCommandResponse(entity.Id);
    }

}
