using Microsoft.AspNetCore.Http;

namespace CVBuilder.Profiles.Requests;

public sealed class CreateProfileRequest(int UserId, string FirstName, string LastName, DateTime BirthDate, string Description, string? PictureName, string CurrentRole)
{
    public int UserId { get; set; } = UserId;
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;
    public DateTime BirthDate { get; set; } = BirthDate;
    public string Description { get; set; } = Description;
    public string? PictureName { get; set; } = PictureName;
    public string CurrentRole { get; set; } = CurrentRole;
}
