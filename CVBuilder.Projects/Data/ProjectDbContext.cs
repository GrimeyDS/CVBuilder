using CVBuilder.Projects.Data.Entities;
using CVBuilder.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Projects.Data;

internal sealed class ProjectDbContext : DbContextBase<ProjectDbContext>
{
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ProjectTagEntity> ProjectTags { get; set; }

    public ProjectDbContext()
    {
    }

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}