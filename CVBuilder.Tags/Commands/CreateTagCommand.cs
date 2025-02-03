using MediatR;

namespace CVBuilder.Tags.Commands;

internal sealed record CreateTagCommand(string Title) : IRequest<CreateTagCommandResponse>
{
    public string Title = Title;
}

internal sealed record CreateTagCommandResponse(int Id);