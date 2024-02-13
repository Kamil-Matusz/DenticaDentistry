using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Core.Entities;
using DenticaDentistry.Application.Services;
using DenticaDentistry.Application.Validators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DenticaDentistry.Application;

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
        //services.AddScoped<IReservationsService,ReservationsService>();
        //services.AddScoped<IDentistsService,DentistsService>();

        services.AddFluentValidation(fv => fv
            .RegisterValidatorsFromAssemblyContaining<DentistDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<ReservationDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<SignUpCommandValidator>()
            .RegisterValidatorsFromAssemblyContaining<SignInCommandValidator>()
            .RegisterValidatorsFromAssemblyContaining<CreateReservationCommandValidator>()
            .RegisterValidatorsFromAssemblyContaining<DeleteReservationCommandValidator>()
            .RegisterValidatorsFromAssemblyContaining<AddLicenseNumberCommandValidator>()
            .RegisterValidatorsFromAssemblyContaining<ChangeDentistServiceNameCommandValidator>()
            .RegisterValidatorsFromAssemblyContaining<ChangeDentistServicePriceCommandValidator>()
            .RegisterValidatorsFromAssemblyContaining<ChangePasswordCommandValidator>()
            .RegisterValidatorsFromAssemblyContaining<DeleteDentistServiceCommandValidator>()
        );
        
        var applicationAssembly = typeof(ICommandHandler<>).Assembly;

        services.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.AddSingleton<IClock, Clock>();
        services.AddScoped<IEmailService,EmailService>();
        services.AddScoped<IReservationsService, ReservationsService>();
        
        return services;
    }
}