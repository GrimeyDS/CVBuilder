using CVBuilder.Profiles.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Profiles;

public static class ProfileModuleConfig
{
    public static IServiceCollection AddProfileModule(this IServiceCollection services, string? dbConnectionString)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProfileModuleConfig).Assembly));
        services.AddDbContext<ProfileDbContext>(o => o.UseSqlServer(dbConnectionString));

        return services;
    }
}