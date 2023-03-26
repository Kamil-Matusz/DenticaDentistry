using System.Net.Http.Headers;
using Dentica_Dentistry.Infrastructure.Auth;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Security;
using DenticaDentistry.Application.Services;
using DenticaDentistry.Infrastructure.Auth;
using DenticaDentistry.Infrastructure.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Xunit;

namespace DenticaDentistry.IntegrationTests.Controllers;

[Collection("api")]
public abstract class BaseControllerTests : IClassFixture<OptionsProvider>
{
    protected HttpClient Client { get; }
    private readonly IAuthenticator _authenticator;

    protected JwtDto Authorize(Guid userId, string role)
    {
        var jwt = _authenticator.CreateToken(userId, role);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

        return jwt;
    }
    
    public BaseControllerTests(OptionsProvider optionsProvider)
    {
        var app = new DentistDentistryTestApp();
        Client = app.Client;
        var postgresOptions = optionsProvider.Get<DatabaseOptions>("database");
        var authOptions = optionsProvider.Get<AuthOptions>("auth");
        _authenticator = new Authenticator(new OptionsWrapper<AuthOptions>(authOptions), new Clock());
    }
}