using MediatR;

namespace CVBuilder.Profiles.Commands;

internal sealed record DeleteProfileCommand(int ProfileId) : IRequest<DeleteProfileCommandResponse>
{
    public int ProfileId = ProfileId;
}

internal sealed record DeleteProfileCommandResponse(int Id, string PictureUrl);
