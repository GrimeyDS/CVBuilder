using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Files;

public static class FilesModuleConfig
{
    public static IServiceCollection AddFileModule(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(FilesModuleConfig).Assembly));
        return services;
    }
}