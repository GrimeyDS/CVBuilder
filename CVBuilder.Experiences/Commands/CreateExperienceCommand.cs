using CVBuilder.Experiences.Constants;
using MediatR;

namespace CVBuilder.Experiences.Commands;

internal sealed record CreateExperienceCommand(int ProfileId, string Title, string Description, ExperienceTypes Type, DateTime StartDate, DateTime? EndDate) : IRequest<CreateExperienceCommandResponse>
{
    public int ProfileId = ProfileId;
    public string Title = Title;
    public string Description = Description;
    public ExperienceTypes Type = Type;
    public DateTime StartDate = StartDate;
    public DateTime? EndDate = EndDate;
}

internal sealed record CreateExperienceCommandResponse(int Id);