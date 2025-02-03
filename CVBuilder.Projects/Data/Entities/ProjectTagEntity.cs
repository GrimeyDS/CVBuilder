using CVBuilder.Shared.Data;

namespace CVBuilder.Projects.Data.Entities;

internal class ProjectTagEntity : EntityBase
{
    public int ProjectId { get; set; }
    public int TagId { get; set; }
}
