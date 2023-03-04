using Dentica_Dentistry.Core.Entities;
using Dentica_Dentistry.Core.Exceptions;
using Shouldly;
using Xunit;

namespace DenticaDentistry.UnitTests.Entities;

public class DentistIndustryTests
{
    private readonly DentistIndustry _dentistIndustry;
    private readonly DateTime _now;

    public DentistIndustryTests()
    {
        _now = new DateTime(2023, 03, 04);
        _dentistIndustry = new DentistIndustry(1, "Leczenie Kanałowe", 100.00, "Leczenie martwego zęba");
    }
    
    [Theory]
    [InlineData("2023-02-20")]
    [InlineData("2023-03-02")]
    public void given_invalid_date_add_reservation_should_fail(string dateString)
    {
        // Arrange
        var now = new DateTime(2023, 03, 04);
        // var invalidReservationDate = new DateTime(2023,03,01);
        var invalidReservationDate = DateTime.Parse(dateString);
        var dentistIndustry = new DentistIndustry(1, "Leczenie Kanałowe", 100.00, "Leczenie martwego zęba");
        var reservation = new Reservation(Guid.NewGuid(),dentistIndustry.DentistIndustryId, "John Doe", invalidReservationDate);
        
        // Act
        var exception = Record.Exception(() => dentistIndustry.AddReservation(reservation));
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidReservationDateException>();
    }

    [Fact]
    public void create_reservation_should_succeed()
    {
        // Arrange
        var reservationDate = new DateTime(2023, 03, 10);
        var reservation = new Reservation(Guid.NewGuid(), _dentistIndustry.DentistIndustryId, "John Doe", reservationDate);

        // Act
        _dentistIndustry.AddReservation(reservation);
        
        // Assert
        _dentistIndustry.Reservations.ShouldHaveSingleItem();
    }
}