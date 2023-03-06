using Dentica_Dentistry.Application.Repositories;
using Dentica_Dentistry.Infrastructure.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace Dentica_Dentistry.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IReservationRepository, InMemoryReservationRepository>();
        services.AddPostgres();
        return services;
    }
}