using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;


namespace DenticaDentistry.IntegrationTests;

internal sealed class DentistDentistryTestApp : WebApplicationFactory<Program>
{
    public HttpClient Client { get; }
    
    public DentistDentistryTestApp()
    {
        Client = WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("test");
        }).CreateClient();

    }
}