using Dentica_Dentistry.Application.Services;
using Dentica_Dentistry.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Dentica_Dentistry.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        /*services.AddSingleton<IEnumerable<DentistIndustry>>(new List<DentistIndustry>
        {
            new DentistIndustry(1, "Lakierowanie zębow", 100.00, "Naprawa szkliwa zębów"),
            new DentistIndustry(2, "Czyszczenie Kamienia", 200.00, "Czyszczenie zębów z nasady kamieniowej"),
            new DentistIndustry(3, "Leczenie Kanałowe", 100.00, "Usuwanie obumarłych fragmentów zębów"),
            new DentistIndustry(4, "Leczenie Próchnicy", 100.00, "Usuwanie prochnicy z zęba"),
            new DentistIndustry(5, "Wizyta Kontrolna", 50.00, "Kontrolne badanie zębów"),
        });*/
        services.AddScoped<IReservationsService,ReservationsService>();
        services.AddScoped<IDentistsService,DentistsService>();
        services.AddScoped<IClock, Clock>();
        return services;
    }
}