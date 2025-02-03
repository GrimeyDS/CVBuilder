using MediatR;

namespace CVBuilder.Experiences.Commands;

internal sealed record UpdateExperienceCommand(int Id, int ProfileId, string Title, string Description, DateTime StartDate, DateTime? EndDate) : IRequest<UpdateExperienceCommandResponse>
{
    public int Id = Id;
    public int ProfileId = ProfileId;
    public string Title = Title;
    public string Description = Description;
    public DateTime StartDate = StartDate;
    public DateTime? EndDate = EndDate;
}

internal sealed record UpdateExperienceCommandResponse(int Id);