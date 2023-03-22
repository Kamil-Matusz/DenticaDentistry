using System.Net;
using System.Net.Http.Json;
using DenticaDentistry.Application.Commands;
using Shouldly;
using Xunit;

namespace DenticaDentistry.IntegrationTests.Controllers;

public class DentistIndustriesControllerTests : BaseControllerTests,IDisposable
{

    [Fact]
    public async Task post_new_dentist_industry_should_return_no_content_status_code()
    {
        var command = new CreateDentistService(10, "Wybielanie", 120.00, "Wybielanie zębów");
        var response = await Client.PostAsJsonAsync("services",command);
        
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
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