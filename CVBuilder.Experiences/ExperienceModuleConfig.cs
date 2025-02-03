using CVBuilder.Experiences.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Experiences;

public static class ExperienceModuleConfig
{
    public static IServiceCollection AddExperienceModule(this IServiceCollection services, string? dbConnectionString)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ExperienceModuleConfig).Assembly));
        services.AddDbContext<ExperienceDbContext>(o => o.UseSqlServer(dbConnectionString));

        return services;
    }
}