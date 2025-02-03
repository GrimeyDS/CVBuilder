using CVBuilder.Shared.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVBuilder.Projects.Data.Entities;

internal class ProjectEntity : EntityBase
{
    public int ProfileId { get; set; }
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Customer { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(250)]
    public string PictureUrl { get; set; } = string.Empty;

    [NotMapped]
    public int Duration
    {
        get
        {
            if (EndDate.HasValue)
                return EndDate.Value.Year - StartDate.Year;
            else
                return DateTime.Now.Year - StartDate.Year;
        }
    }
}
