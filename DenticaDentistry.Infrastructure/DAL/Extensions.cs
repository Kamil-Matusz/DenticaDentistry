using Dentica_Dentistry.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dentica_Dentistry.Infrastructure.DAL;

internal static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services)
    {
        const string connectionString = "Host=localhost;Database=DenticaDentistry;Username=postgres;Password=";
        services.AddDbContext<DenticaDentistryDbContext>(x => x.UseNpgsql(connectionString));
        services.AddScoped<IReservationRepository, PostgresReservationRepository>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    } 
}