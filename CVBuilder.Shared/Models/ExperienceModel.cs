namespace CVBuilder.Shared.Models;

public class ExperienceModel
{
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
