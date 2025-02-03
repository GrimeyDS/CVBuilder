using CVBuilder.Users.Commands;
using CVBuilder.Users.Constants;
using CVBuilder.Users.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Users.Handlers;

internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse>
{
    private readonly UserDbContext _context;

    public UpdateUserCommandHandler(UserDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var oldUser = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(request.Id), cancellationToken);

        if (oldUser == null)
            throw new Exception(ErrorMessages.UserNotFound);

        if (oldUser.EntraId != request.EntraId)
            throw new Exception(ErrorMessages.EntraIdInvalid);
        
        oldUser.Email = request.Email;

        _context.Users.Update(oldUser);
        await _context.SaveChangesAsync(cancellationToken);
        return new UpdateUserCommandResponse(oldUser.Id);
    }
}