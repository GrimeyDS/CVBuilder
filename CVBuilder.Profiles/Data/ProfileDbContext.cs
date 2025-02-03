using CVBuilder.Profiles.Data.Entities;
using CVBuilder.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Profiles.Data;

internal sealed class ProfileDbContext : DbContextBase<ProfileDbContext>
{
    public DbSet<ProfileEntity> Profiles { get; set; }
    public DbSet<ProfileTagEntity> ProfileTags { get; set; }

    public ProfileDbContext()
    {
    }

    public ProfileDbContext(DbContextOptions<ProfileDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}