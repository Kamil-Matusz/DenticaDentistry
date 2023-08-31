using System.Net;
using System.Net.Http.Json;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Application.Services;
using Shouldly;
using Xunit;

namespace DenticaDentistry.IntegrationTests.Controllers;

public class ServiceTypesControllerTests : BaseControllerTests,IDisposable
{
    private readonly TestDatabase _testDatabase;
    public ServiceTypesControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
    
    [Fact]
    public async Task post_new_serviceType_should_return_ok_or_unauthorized_status_code()
    {
        var command = new CreateServiceType(1, "Test Service Type");
        var response = await Client.PostAsJsonAsync("serviceTypes",command);
        
        response.StatusCode.ShouldBeOneOf(HttpStatusCode.OK,HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task GetAllDentistServicesTypes_ShouldReturnOkWithTypesList()
    {
        // Arrange
        var query = new GetAllServiceTypes();

        // Act
        var response = await Client.GetAsync("serviceTypes");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var types = await response.Content.ReadFromJsonAsync<ServiceTypeDto[]>();
        types.ShouldNotBeNull();
    }
}