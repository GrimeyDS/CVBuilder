using CVBuilder.Shared.Data;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Profiles.Data.Entities;

internal class ProfileEntity : EntityBase
{
    public int UserId { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    [MaxLength(250)]
    public string CurrentRole { get; set; } = string.Empty;
    [MaxLength(540)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(250)]
    public string PictureUrl { get; set; } = string.Empty;
    public bool IsFirstTimeSetup { get; set; }
}
