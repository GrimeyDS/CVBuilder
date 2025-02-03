using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CVBuilder.Shared.Data;

public abstract class DbContextBase<T> : DbContext where T : DbContext
{
	protected DbContextBase()
	{
	}

	protected DbContextBase(DbContextOptions<T> options)
		: base(options)
	{
	}

	public override int SaveChanges()
	{
		AddMetadataAsync().GetAwaiter().GetResult();
		return base.SaveChanges();
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		await AddMetadataAsync();
		return await base.SaveChangesAsync(cancellationToken);
	}

    private Task AddMetadataAsync()
    {
        IEnumerable<EntityEntry> entries = ChangeTracker.Entries<EntityBase>();

        foreach (var entry in entries)
        {
            if (entry.Entity is EntityBase baseEntity)
            {
                if (entry.State == EntityState.Added)
                {
                    baseEntity.CreatedOn = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    baseEntity.ModifiedOn = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Deleted)
                {
                    baseEntity.IsDeleted = true;
                }
            }
        }

        return Task.CompletedTask;
    }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		foreach (var entityType in modelBuilder.Model.GetEntityTypes())
		{
            var entityAsParameter = Expression.Parameter(entityType.ClrType, "entity");

            Expression? isNotDeletedFilter = null;

            if (typeof(EntityBase).IsAssignableFrom(entityType.ClrType))
            {
                var isNotDeleted = Expression.Equal(
                                        Expression.Property(entityAsParameter, "IsDeleted"),
                                        Expression.Constant(false)
                                   );

                isNotDeletedFilter = isNotDeleted;
            }

            if (isNotDeletedFilter != null)
            {
                var isNotDeletedLambda = Expression.Lambda(isNotDeletedFilter, entityAsParameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(isNotDeletedLambda);
            }
        }
    }
}