namespace CVBuilder.Shared.Models;

public class ProjectModel
{
    public int Id { get; set; } 
    public int ProfileId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Customer { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public List<TagModel> Tags { get; set; } = new List<TagModel>();
}
