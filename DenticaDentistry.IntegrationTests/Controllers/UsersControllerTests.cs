using System.Net;
using System.Net.Http.Json;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Services;
using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.ValueObjects;
using DenticaDentistry.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace DenticaDentistry.IntegrationTests.Controllers;

public class UsersControllerTests : BaseControllerTests,IDisposable
{

    [Fact]
    public async Task post_users_should_return_no_content_status_code()
    {
        
        var command = new SignUp(Guid.Empty, "test-user@onet.pl", "test-user", "secret","Test Doe", "user");
        var response = await Client.PostAsJsonAsync("users/signUp", command);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task post_sign_in_should_return_ok_status_code()
    {
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        const string password = "password";

        var user = new User(Guid.NewGuid(),"test1@test.com","testUser",passwordManager.Secure(password),"Test User","user");
        
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.SaveChangesAsync();
        
        var command = new SignIn(user.Email, password);
        var response = await Client.PostAsJsonAsync("users/signIn", command);
        var jwt = await response.Content.ReadFromJsonAsync<JwtDto>();

        jwt.ShouldNotBeNull();
        jwt.AccessToken.ShouldNotBeNullOrWhiteSpace();

    }

    [Fact]
    public async Task get_users_me_should_return_ok_status_code_and_user()
    {
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        const string password = "password";

        var user = new User(Guid.NewGuid(),"test1@test.com","testUser",passwordManager.Secure(password),"Test User","user");

        await _testDatabase.DbContext.Database.MigrateAsync();
        await _testDatabase.DbContext.Users.AddAsync(user);
        await _testDatabase.DbContext.SaveChangesAsync();
        
        Authorize(user.UserId, user.Role);
        var userDto = await Client.GetFromJsonAsync<UserDto>("users/me");
        
        userDto.ShouldNotBeNull();
        userDto.UserId.ShouldBe(user.UserId.Value);
    }

    
    private readonly TestDatabase _testDatabase;
    
    public UsersControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
}