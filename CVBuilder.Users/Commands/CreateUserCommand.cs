using MediatR;

namespace CVBuilder.Users.Commands
{
    internal sealed record CreateUserCommand(string entraId, string email) : IRequest<CreateUserCommandResponse>
    {
        public string EntraId => entraId;
        public string Email => email;
    }

    internal sealed record CreateUserCommandResponse(int Id);
}
