using Microsoft.AspNetCore.Http;

namespace CVBuilder.Projects.Requests;

public sealed class UpdateProjectRequest(string Title, string Customer, DateTime StartDate, DateTime? EndDate, string Description, string? PictureName)
{
    public string Title { get; set; } = Title;
    public string Customer { get; set; } = Customer;
    public DateTime StartDate { get; set; } = StartDate;
    public DateTime? EndDate { get; set; } = EndDate;
    public string Description { get; set; } = Description;
    public string? PictureName { get; set; } = PictureName;
}
