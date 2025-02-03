using MediatR;

namespace CVBuilder.Projects.Commands;

internal sealed record UpdateProjectTagsCommand(int Id, List<int> TagIds) : IRequest<UpdateProjectTagsCommandResponse>
{
    public int Id = Id;
    public List<int> TagIds = TagIds;
}

internal sealed record UpdateProjectTagsCommandResponse(int Id);