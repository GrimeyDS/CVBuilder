using CVBuilder.Experiences;
using CVBuilder.Tags;
using CVBuilder.Middleware;
using CVBuilder.Users;
using FluentValidation;
using MediatR;
using CVBuilder.Profiles;
using CVBuilder.Projects;
using CVBuilder.Pdf;
using CVBuilder.Files;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CVBuilder;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add modules
        builder.Services.AddUserModule(builder.Configuration.GetConnectionString("DefaultConnection"));
        builder.Services.AddExperienceModule(builder.Configuration.GetConnectionString("DefaultConnection"));
        builder.Services.AddProfileModule(builder.Configuration.GetConnectionString("DefaultConnection"));
        builder.Services.AddProjectModule(builder.Configuration.GetConnectionString("DefaultConnection"));
        builder.Services.AddTagModule(builder.Configuration.GetConnectionString("DefaultConnection"));
        builder.Services.AddPDFModule(builder.Configuration.GetConnectionString("DefaultConnection"));
        builder.Services.AddFileModule();
        
        // Add Mediatr
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        
        // Add API controllers
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add ProblemDetails support
        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();
        var assemblies = new[] { ExperienceAssemblyReference.Assembly, ProfileAssemblyReference.Assembly, UserAssemblyReference.Assembly, ProjectsAssemblyReference.Assembly, TagAssemblyReference.Assembly, PDFAssemblyReference.Assembly, FileAssemblyReference.Assembly};
        builder.Services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            });
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseExceptionHandler();
        app.UseStatusCodePages();

        app.UseHttpsRedirection();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
 
        app.Run();
    }
}