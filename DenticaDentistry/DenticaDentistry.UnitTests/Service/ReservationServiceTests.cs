﻿using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.Services;
using DenticaDentistry.Core.Entities;
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
            new DentistIndustry(1, "Lakierowanie zębow", 100.00, "Naprawa szkliwa zębów",1),
            new DentistIndustry(2, "Czyszczenie Kamienia", 200.00, "Czyszczenie zębów z nasady kamieniowej",2),
            new DentistIndustry(3, "Leczenie Kanałowe", 100.00, "Usuwanie obumarłych fragmentów zębów",3),
            new DentistIndustry(4, "Leczenie Próchnicy", 100.00, "Usuwanie prochnicy z zęba",3),
            new DentistIndustry(5, "Wizyta Kontrolna", 50.00, "Kontrolne badanie zębów",3),
        };
    }
    
    [Fact]
    public async Task add_reservation_should_succeed()
    {
        // Arrange
        var command = new CreateReservation(Guid.NewGuid(), 1, "John Doe", DateTime.UtcNow.AddDays(1),Guid.NewGuid(),Guid.NewGuid());
        
        // Act
        var reservationId = await _reservationsService.CreateReservationAsync(command);
        
        // Assert
        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);

    }
}