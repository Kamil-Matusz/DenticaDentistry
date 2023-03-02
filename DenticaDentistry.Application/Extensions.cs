using Dentica_Dentistry.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dentica_Dentistry.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IReservationsService,ReservationsService>();
        return services;
    }
}