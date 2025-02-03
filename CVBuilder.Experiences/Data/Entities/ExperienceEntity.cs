using CVBuilder.Shared.Data;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Experiences.Data.Entities;

internal class ExperienceEntity : EntityBase
{
    public int ProfileId { get; set; }
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(250)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
