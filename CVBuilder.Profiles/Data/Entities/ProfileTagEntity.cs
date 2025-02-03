using CVBuilder.Shared.Data;

namespace CVBuilder.Profiles.Data.Entities;

internal class ProfileTagEntity : EntityBase
{
    public int ProfileId { get; set; }
    public int TagId { get; set; }
}
