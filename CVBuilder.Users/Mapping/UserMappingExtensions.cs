using CVBuilder.Users.Data.Entities;
using CVBuilder.Users.Models;

namespace CVBuilder.Users.Mapping;

internal static class UserMappingExtensions
{
    public static UserModel ToModel(this UserEntity entity)
    {
        return new UserModel
        {
            Id = entity.Id,
            Email = entity.Email
        };
    }
}