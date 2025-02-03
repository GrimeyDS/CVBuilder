using MediatR;

namespace CVBuilder.Experiences.Commands;

internal sealed record DeleteExperienceCommand(int Id) : IRequest<DeleteExperienceCommandResponse>
{
    public int Id = Id;
}

internal sealed record DeleteExperienceCommandResponse(int Id);
