using Dentica_Dentistry.Application.Commands;
using Dentica_Dentistry.Application.Services;
using Dentica_Dentistry.Core.Entities;
using Shouldly;
using Xunit;

namespace DenticaDentistry.UnitTests.Service;

public class ReservationServiceTests
{
    private readonly IReservationsService _reservationsService;
    private readonly List<DentistIndustry> _dentistIndustriesList;
    

    public ReservationServiceTests(IReservationsService reservationsService)
    {
        _reservationsService = reservationsService;
        _dentistIndustriesList = new List<DentistIndustry>()
        {
            new DentistIndustry(1, "Lakierowanie zębow", 100.00, "Naprawa szkliwa zębów"),
            new DentistIndustry(2, "Czyszczenie Kamienia", 200.00, "Czyszczenie zębów z nasady kamieniowej"),
            new DentistIndustry(3, "Leczenie Kanałowe", 100.00, "Usuwanie obumarłych fragmentów zębów"),
            new DentistIndustry(4, "Leczenie Próchnicy", 100.00, "Usuwanie prochnicy z zęba"),
            new DentistIndustry(5, "Wizyta Kontrolna", 50.00, "Kontrolne badanie zębów"),
        };
    }
    
    [Fact]
    public void add_reservation_should_succeed()
    {
        // Arrange
        var command = new CreateReservation(Guid.NewGuid(), 1, "John Doe", DateTime.UtcNow.AddDays(1));
        
        // Act
        var reservationId = _reservationsService.CreateReservation(command);
        
        // Assert
        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);

    }
}