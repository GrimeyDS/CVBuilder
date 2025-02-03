using CVBuilder.Users.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Users;

public static class UserModuleConfig
{
    public static IServiceCollection AddUserModule(this IServiceCollection services, string? dbConnectionString)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UserModuleConfig).Assembly));
        services.AddDbContext<UserDbContext>(o => o.UseSqlServer(dbConnectionString));

        return services;
    }
}