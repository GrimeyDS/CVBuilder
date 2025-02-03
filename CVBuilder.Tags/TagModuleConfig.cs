using CVBuilder.Tags.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Tags;

public static class TagModuleConfig
{
    public static IServiceCollection AddTagModule(this IServiceCollection services, string? dbConnectionString)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TagModuleConfig).Assembly));
        services.AddDbContext<TagDbContext>(o => o.UseSqlServer(dbConnectionString));

        return services;
    }
}