namespace CVBuilder.Experiences.Requests;

public sealed class UpdateExperienceRequest(int ProfileId, string Title, string Description, DateTime StartDate, DateTime? EndDate)
{
    public int ProfileId { get; set; } = ProfileId;
    public string Title { get; set; } = Title;
    public string Description { get; set; } = Description;
    public DateTime StartDate { get; set; } = StartDate;
    public DateTime? EndDate { get; set; } = EndDate;
}