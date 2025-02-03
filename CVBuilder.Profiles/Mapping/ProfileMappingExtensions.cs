using CVBuilder.Profiles.Data.Entities;
using CVBuilder.Shared.Models;

namespace CVBuilder.Profiles.Mapping;

internal static class ProfileMappingExtensions
{
    public static ProfileModel ToModel(this ProfileEntity entity)
    {
        return new ProfileModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            BirthDate = entity.BirthDate,
            CurrentRole = entity.CurrentRole,
            Description = entity.Description,
            PictureUrl = entity.PictureUrl,
        };
    }
}