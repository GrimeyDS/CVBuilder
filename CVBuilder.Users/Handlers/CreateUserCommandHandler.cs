using CVBuilder.Users.Commands;
using CVBuilder.Users.Constants;
using CVBuilder.Users.Data;
using CVBuilder.Users.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Users.Handlers;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
{
    private readonly UserDbContext _context;

    public CreateUserCommandHandler(UserDbContext context)
    {
        _context = context;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new UserEntity
        {
            EntraId = request.EntraId.Trim(),
            Email = request.Email.Trim().ToLower()
        };

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.EntraId == user.EntraId || u.Email == user.Email, cancellationToken);

        if (existingUser != null)
            throw new Exception(ErrorMessages.UserExists);

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateUserCommandResponse(user.Id);
    }
}
