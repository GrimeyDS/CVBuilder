using CVBuilder.Projects.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Projects;

public static class ProjectModuleConfig
{
    public static IServiceCollection AddProjectModule(this IServiceCollection services, string? dbConnectionString)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProjectModuleConfig).Assembly));
        services.AddDbContext<ProjectDbContext>(o => o.UseSqlServer(dbConnectionString));

        return services;
    }
}