using DenticaDentistry.Application.Repositories;
using DenticaDentistry.Core.Repositories;
using DenticaDentistry.Infrastructure.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DenticaDentistry.Infrastructure.DAL;

internal static class Extensions
{
    private const string SectionName = "database";
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<DatabaseOptions>(section);
        var options = configuration.GetOptions<DatabaseOptions>(SectionName);
        
        
        services.AddDbContext<DenticaDentistryDbContext>(x => x.UseNpgsql(options.connectionString));
        services.AddScoped<IReservationRepository, PostgresReservationRepository>();
        services.AddScoped<IDentistIndustryRepository, PostgresDentistIndustryRepository>();
        services.AddHostedService<DatabaseInitializer>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    } 
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}