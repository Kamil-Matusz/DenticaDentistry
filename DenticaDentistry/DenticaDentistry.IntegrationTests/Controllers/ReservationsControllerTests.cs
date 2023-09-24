using System.Net;
using System.Net.Http.Json;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Application.Services;
using Shouldly;
using Xunit;

namespace DenticaDentistry.IntegrationTests.Controllers;

public class ReservationsControllerTests : BaseControllerTests,IDisposable
{

    [Fact]
    public async Task post_new_reservation_should_return_no_content_status_code()
    {
        // Arrange
        var clock = new Clock();
        var command = new CreateReservation(Guid.NewGuid(), 3, "John Doe", clock.CurrentDate().AddHours(1),Guid.NewGuid(),Guid.NewGuid());
        
        // Act
        var response = await Client.PostAsJsonAsync("reservations",command);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task GetAllReservations_ShouldReturnOkWithReservations()
    {
        // Arrange
        var query = new GetAllReservations();

        // Act
        var response = await Client.GetAsync("reservations");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var reservations = await response.Content.ReadFromJsonAsync<ReservationDto[]>();
        reservations.ShouldNotBeNull();
    }
    
    private readonly TestDatabase _testDatabase;
    
    public ReservationsControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
}