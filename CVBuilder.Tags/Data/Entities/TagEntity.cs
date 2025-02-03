using CVBuilder.Shared.Data;
using System.ComponentModel.DataAnnotations;


namespace CVBuilder.Tags.Data.Entities;

internal class TagEntity : EntityBase
{
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
}

