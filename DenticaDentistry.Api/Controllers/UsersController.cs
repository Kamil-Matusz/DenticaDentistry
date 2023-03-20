using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Application.Security;
using DenticaDentistry.Core.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dentica_Dentistry.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly ICommandHandler<SignUp> _signUpHandler;
    private readonly ICommandHandler<SignIn> _signInHandler;
    private readonly ITokenStorage _tokenStorage;
    private readonly IQueryHandler<GetAllUsers, IEnumerable<UserDto>> _getAllUsersHandler;
    private readonly IQueryHandler<GetUser, UserDto> _getUserHandler;
    
    public UsersController(ICommandHandler<SignUp> signUpHandler, IQueryHandler<GetAllUsers, IEnumerable<UserDto>> getAllUsersHandler, IQueryHandler<GetUser, UserDto> getUserHandler, ICommandHandler<SignIn> signInHandler,ITokenStorage tokenStorage)
    {
        _signUpHandler = signUpHandler;
        _getAllUsersHandler = getAllUsersHandler;
        _getUserHandler = getUserHandler;
        _signInHandler = signInHandler;
        _tokenStorage = tokenStorage;
    }

    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers([FromQuery] GetAllUsers query)
        => Ok(await _getAllUsersHandler.HandlerAsync(query));
    
    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetInfoAboutMe()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }
        var userId = Guid.Parse(User.Identity?.Name);
        var user = await _getUserHandler.HandlerAsync(new GetUser { UserId = userId });
        return user;
    }
    
    [Authorize(Roles = "admin")]
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<UserDto>> GetUser(Guid userId)
    {
        var user = await _getUserHandler.HandlerAsync(new GetUser {UserId = userId});
        if (user is null)
        {
            return NotFound();
        }

        return user;
    }
    
    [HttpPost("signUp")]
    public async Task<ActionResult> SignUp(SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await _signUpHandler.HandlerAsync(command);

        return NoContent();
    }

    [HttpPost("signIn")]
    public async Task<ActionResult<JwtDto>> SignIn(SignIn command)
    {
        await _signInHandler.HandlerAsync(command);
        var jwt = _tokenStorage.GetToken();
        return Ok(jwt);
    }
}