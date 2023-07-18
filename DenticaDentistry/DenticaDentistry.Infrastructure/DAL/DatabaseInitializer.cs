using DenticaDentistry.Application.Security;
using DenticaDentistry.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DenticaDentistry.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPasswordManager _passwordManager;

    public DatabaseInitializer(IServiceProvider serviceProvider,IPasswordManager passwordManager)
    {
        _serviceProvider = serviceProvider;
        _passwordManager = passwordManager;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        // auto migration to the database
        using (var scope = _serviceProvider.CreateScope()) {
            var dbContext = scope.ServiceProvider.GetRequiredService<DenticaDentistryDbContext>();
            dbContext.Database.Migrate();

            var servicesTypes = dbContext.ServiceTypes.ToList();
            if (!servicesTypes.Any())
            {
                servicesTypes = new List<ServiceType>()
                {
                    new ServiceType(1, "Chirurgia dentystyczna"),
                    new ServiceType(2, "Implanty"),
                    new ServiceType(3, "Profilaktyka"),
                };
                dbContext.ServiceTypes.AddRange(servicesTypes);
                dbContext.SaveChanges();
            }
            
            var dentistIndustries = dbContext.DentistIndustries.ToList();
            if (!dentistIndustries.Any())
            {
                dentistIndustries = new List<DentistIndustry>(){
                    new DentistIndustry(1, "Lakierowanie zębow", 100.00, "Naprawa szkliwa zębów",1),
                    new DentistIndustry(2, "Czyszczenie Kamienia", 200.00, "Czyszczenie zębów z nasady kamieniowej",3),
                    new DentistIndustry(3, "Leczenie Kanałowe", 100.00, "Usuwanie obumarłych fragmentów zębów",1),
                    new DentistIndustry(4, "Leczenie Próchnicy", 100.00, "Usuwanie prochnicy z zęba",3),
                    new DentistIndustry(5, "Wizyta Kontrolna", 50.00, "Kontrolne badanie zębów",3),
                };
                dbContext.DentistIndustries.AddRange(dentistIndustries);
                dbContext.SaveChanges();
            }

            var users = dbContext.Users.ToList();
            const string password = "password";
            if (!users.Any())
            {
                users = new List<User>()
                {
                    new User(Guid.NewGuid(), "admin@test.com", "Admin", _passwordManager.Secure(password), "Admin Account",
                        "admin")
                };
                dbContext.Users.AddRange(users);
                dbContext.SaveChanges();
            }
        }
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}