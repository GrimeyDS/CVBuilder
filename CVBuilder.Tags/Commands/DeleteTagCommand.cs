using MediatR;

namespace CVBuilder.Tags.Commands;

internal sealed record DeleteTagCommand(int TagId, int OriginId) : IRequest<DeleteTagCommandResponse>
{
    public int TagId = TagId;
    public int OriginId = OriginId;
}

internal sealed record DeleteTagCommandResponse(int Id);
