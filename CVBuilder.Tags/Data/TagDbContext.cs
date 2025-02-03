using CVBuilder.Shared.Data;
using CVBuilder.Tags.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Tags.Data;

internal sealed class TagDbContext : DbContextBase<TagDbContext>
{
    public DbSet<TagEntity> Tags { get; set; }

    public TagDbContext()
    {
    }

    public TagDbContext(DbContextOptions<TagDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}