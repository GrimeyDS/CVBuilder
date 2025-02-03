using CVBuilder.Users.Models;
using MediatR;

namespace CVBuilder.Users.Commands;

internal sealed record UpdateUserCommand(int id, string email, string entraId) : IRequest<UpdateUserCommandResponse>
{
    public int Id => id;
    public string Email => email;
    public string EntraId => entraId;
}

internal sealed record UpdateUserCommandResponse(int Id);
