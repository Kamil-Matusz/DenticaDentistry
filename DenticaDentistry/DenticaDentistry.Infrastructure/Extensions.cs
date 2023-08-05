using Dentica_Dentistry.Infrastructure.Auth;
using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Infrastructure.DAL;
using DenticaDentistry.Application.Repositories;
using DenticaDentistry.Core.Repositories;
using DenticaDentistry.Infrastructure.Exceptions;
using DenticaDentistry.Infrastructure.Logging;
using DenticaDentistry.Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DenticaDentistry.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IReservationRepository, InMemoryReservationRepository>();
        services.AddPostgres(configuration);
        services.AddCustomLogging();
        services.AddSingleton<ExceptionMiddleware>();
        services.AddSecurity();
        services.AddAuth(configuration);
        services.AddHttpContextAccessor();
        
        var infrastructureAssembly = typeof(AppOptions).Assembly;

        services.Scan(s => s.FromAssemblies(infrastructureAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Dentica Dentistry",
                Version = "V1"
            });
        });
        
        return services;
    }
    
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}