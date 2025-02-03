using CVBuilder.Experiences.Data.Entities;
using CVBuilder.Shared.Models;

namespace CVBuilder.Experiences.Mapping;

internal static class ExperienceMappingExtensions
{
    public static ExperienceModel ToModel(this ExperienceEntity entity)
    {
        return new ExperienceModel
        {
            Id = entity.Id,
            ProfileId = entity.ProfileId,
            Title = entity.Title,
            Description = entity.Description,
            Type = entity.Type.ToString(),
            StartDate = entity.StartDate,
            EndDate = entity.EndDate
        };
    }
}