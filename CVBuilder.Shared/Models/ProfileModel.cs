namespace CVBuilder.Shared.Models;

public class ProfileModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string CurrentRole { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public List<TagModel> Tags { get; set; } = new List<TagModel>();
    public List<ExperienceModel> Experiences { get; set; } = new List<ExperienceModel>();
    public List<ProjectModel> Projects { get; set; } = new List<ProjectModel>();

}
