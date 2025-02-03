using System.ComponentModel.DataAnnotations;
using CVBuilder.Shared.Data;

namespace CVBuilder.Users.Data.Entities;

internal class UserEntity : EntityBase
{
    [MaxLength(320)]
    public string Email { get; set; } = string.Empty;

    public string EntraId { get; set; } = string.Empty;
}