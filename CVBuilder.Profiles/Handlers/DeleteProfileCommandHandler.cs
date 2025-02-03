using CVBuilder.Profiles.Commands;
using CVBuilder.Profiles.Constants;
using CVBuilder.Profiles.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Profiles.Handlers;

internal class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, DeleteProfileCommandResponse>
{
    private readonly ProfileDbContext _context;

    public DeleteProfileCommandHandler(ProfileDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteProfileCommandResponse> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == request.ProfileId, cancellationToken);
        if (entity == null)
            throw new Exception(ErrorMessages.ProfileNotFound);

        _context.Profiles.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteProfileCommandResponse(entity.Id, entity.PictureUrl);
    }

}
