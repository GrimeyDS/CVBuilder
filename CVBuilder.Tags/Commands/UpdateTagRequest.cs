using CVBuilder.Tags.Constants;
using MediatR;

namespace CVBuilder.Tags.Commands;

internal sealed record UpdateTagCommand(int TagId, string Title) : IRequest<UpdateTagCommandResponse>
{
    public int TagId = TagId;
    public string Title = Title;
}

internal sealed record UpdateTagCommandResponse(int Id);