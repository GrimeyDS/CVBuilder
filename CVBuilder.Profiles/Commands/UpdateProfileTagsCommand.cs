using MediatR;

namespace CVBuilder.Profiles.Commands;

internal sealed record UpdateProfileTagsCommand(int Id, List<int> TagIds) : IRequest<UpdateProfileTagsCommandResponse>
{
    public int Id = Id;
    public List<int> TagIds = TagIds;
}

internal sealed record UpdateProfileTagsCommandResponse(int Id);