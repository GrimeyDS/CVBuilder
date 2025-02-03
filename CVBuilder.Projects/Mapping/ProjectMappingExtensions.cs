using CVBuilder.Projects.Data.Entities;
using CVBuilder.Shared.Models;

namespace CVBuilder.Projects.Mapping;

internal static class ProjectMappingExtensions
{
    public static ProjectModel ToModel(this ProjectEntity entity)
    {
        return new ProjectModel
        {
            Id = entity.Id,
            ProfileId = entity.ProfileId,
            Title = entity.Title,
            Customer = entity.Customer,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Description = entity.Description,
            PictureUrl = entity.PictureUrl
        };
    }
}