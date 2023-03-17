using DenticaDentistry.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DenticaDentistry.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        // auto migration to the database
        using (var scope = _serviceProvider.CreateScope()) {
            var dbContext = scope.ServiceProvider.GetRequiredService<DenticaDentistryDbContext>();
            dbContext.Database.Migrate();

            var dentistIndustries = dbContext.DentistIndustries.ToList();
            if (!dentistIndustries.Any())
            {
                dentistIndustries = new List<DentistIndustry>(){
                    new DentistIndustry(1, "Lakierowanie zębow", 100.00, "Naprawa szkliwa zębów"),
                    new DentistIndustry(2, "Czyszczenie Kamienia", 200.00, "Czyszczenie zębów z nasady kamieniowej"),
                    new DentistIndustry(3, "Leczenie Kanałowe", 100.00, "Usuwanie obumarłych fragmentów zębów"),
                    new DentistIndustry(4, "Leczenie Próchnicy", 100.00, "Usuwanie prochnicy z zęba"),
                    new DentistIndustry(5, "Wizyta Kontrolna", 50.00, "Kontrolne badanie zębów"),
                };
                dbContext.DentistIndustries.AddRange(dentistIndustries);
                dbContext.SaveChanges();
            }
        }
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}