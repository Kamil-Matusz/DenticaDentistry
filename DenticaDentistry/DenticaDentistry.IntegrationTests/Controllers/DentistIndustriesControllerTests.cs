using System.Net;
using System.Net.Http.Json;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using Shouldly;
using Xunit;

namespace DenticaDentistry.IntegrationTests.Controllers;

public class DentistIndustriesControllerTests : BaseControllerTests,IDisposable
{

    [Fact]
    public async Task post_new_dentist_industry_should_return_no_content_status_code()
    {
        // Arrange
        var command = new CreateDentistService(10, "Wybielanie", 120.00, "Wybielanie zębów",1);
        
        // Act
        var response = await Client.PostAsJsonAsync("services",command);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task GetAllDentistServices_ShouldReturnOkWithServicesList()
    {
        // Arrange
        var query = new GetAllDentistServices();

        // Act
        var response = await Client.GetAsync("services");

        // Assert
        response.StatusCode.ShouldBeOneOf(HttpStatusCode.OK,HttpStatusCode.Forbidden);
        var services = await response.Content.ReadFromJsonAsync<DentistIndustryDto[]>();
        services.ShouldNotBeNull();
    }
    
    private readonly TestDatabase _testDatabase;
    
    public DentistIndustriesControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
}