using System.Net;
using System.Net.Http.Json;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using Shouldly;
using Xunit;

namespace DenticaDentistry.IntegrationTests.Controllers;

public class DentistControllerTests : BaseControllerTests,IDisposable
{
    private readonly TestDatabase _testDatabase;

    public DentistControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
    
    [Fact]
    public async Task GetAllDentist_ShouldReturnOk()
    {
        // Arrange
        var query = new GetAllDentists();

        // Act
        var response = await Client.GetAsync("dentists/getAllDentists");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var types = await response.Content.ReadFromJsonAsync<DentistDto[]>();
        types.ShouldNotBeNull();
    }
}