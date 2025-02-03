
namespace CVBuilder.Experiences.Requests;

public sealed class CreateExperienceRequest(int ProfileId, string Title, string Description, string Type, DateTime StartDate, DateTime? EndDate)
{
    public int ProfileId { get; set; } = ProfileId;
    public string Title { get; set; } = Title;
    public string Description { get; set; } = Description;
    public string Type { get; set; } = Type;
    public DateTime StartDate { get; set; } = StartDate;
    public DateTime? EndDate { get; set; } = EndDate;
}