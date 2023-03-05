using Dentica_Dentistry.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Dentica_Dentistry.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDentistIndustryRepository, InMemoryDentistIndustryRepository>();
        return services;
    }
}