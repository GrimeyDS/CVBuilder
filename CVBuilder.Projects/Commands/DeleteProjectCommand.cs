using MediatR;

namespace CVBuilder.Projects.Commands;

internal sealed record DeleteProjectCommand(int ProjectId) : IRequest<DeleteProjectCommandResponse>
{
    public int ProjectId = ProjectId;
}

internal sealed record DeleteProjectCommandResponse(int Id, string PictureUrl);
