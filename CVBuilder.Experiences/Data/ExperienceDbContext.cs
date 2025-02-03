using CVBuilder.Shared.Data;
using CVBuilder.Experiences.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Experiences.Data;

internal sealed class ExperienceDbContext : DbContextBase<ExperienceDbContext>
{
    public DbSet<ExperienceEntity> Experiences { get; set; }
    
    public ExperienceDbContext()
    {
    }

    public ExperienceDbContext(DbContextOptions<ExperienceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}