using CVBuilder.Pdf.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Pdf;

public static class PDFModuleConfig
{
    public static IServiceCollection AddPDFModule(this IServiceCollection services, string? dbConnectionString)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PDFModuleConfig).Assembly));
        services.AddDbContext<PDFTemplateDbContext>(o => o.UseSqlServer(dbConnectionString));

        return services;
    }
}