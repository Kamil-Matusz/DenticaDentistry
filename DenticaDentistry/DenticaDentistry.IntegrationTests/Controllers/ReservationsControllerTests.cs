using System.Net;
using System.Net.Http.Json;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.Services;
using Shouldly;
using Xunit;

namespace DenticaDentistry.IntegrationTests.Controllers;

public class ReservationsControllerTests : BaseControllerTests,IDisposable
{

    [Fact]
    public async Task post_new_reservation_should_return_no_content_status_code()
    {
        var clock = new Clock();
        var command = new CreateReservation(Guid.NewGuid(), 3, "John Doe", clock.CurrentDate().AddHours(1),Guid.NewGuid());
        var response = await Client.PostAsJsonAsync("reservations",command);
        
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
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