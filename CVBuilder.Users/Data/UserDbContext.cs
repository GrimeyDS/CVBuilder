using CVBuilder.Shared.Data;
using CVBuilder.Users.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Users.Data;

internal sealed class UserDbContext : DbContextBase<UserDbContext>
{
    public DbSet<UserEntity> Users { get; set; }
    
    public UserDbContext()
    {
    }

    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}