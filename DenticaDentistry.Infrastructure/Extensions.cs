using Dentica_Dentistry.Application.Repositories;
using Dentica_Dentistry.Core.Repositories;
using Dentica_Dentistry.Infrastructure.DAL;
using Dentica_Dentistry.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dentica_Dentistry.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IReservationRepository, InMemoryReservationRepository>();
        services.AddPostgres(configuration);
        services.AddSingleton<ExceptionMiddleware>();
        return services;
    }
    
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        
        return app;
    }
}