using CVBuilder.Tags.Data.Entities;
using CVBuilder.Shared.Models;

namespace CVBuilder.Tags.Mapping;

internal static class TagMappingExtensions
{
    public static TagModel ToModel(this TagEntity entity)
    {
        return new TagModel
        {
            Id = entity.Id,
            Title = entity.Title,
        };
    }
}