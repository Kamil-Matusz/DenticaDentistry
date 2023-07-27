using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Application.Security;
using DenticaDentistry.Application.Services;
using DenticaDentistry.Core.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    private readonly IQueryHandler<GetUserByName, UserDto> _getUserByNameHandler;
    private readonly IEmailService _emailService;

    public UsersController(ICommandHandler<SignUp> signUpHandler, IQueryHandler<GetAllUsers, IEnumerable<UserDto>> getAllUsersHandler, IQueryHandler<GetUser, UserDto> getUserHandler, ICommandHandler<SignIn> signInHandler,ITokenStorage tokenStorage, IQueryHandler<GetUserByName, UserDto> getUserByNameHandler, IEmailService emailService)
    {
        _signUpHandler = signUpHandler;
        _getAllUsersHandler = getAllUsersHandler;
        _getUserHandler = getUserHandler;
        _signInHandler = signInHandler;
        _tokenStorage = tokenStorage;
        _getUserByNameHandler = getUserByNameHandler;
        _emailService = emailService;
    }

    [Authorize(Roles = "admin")]
    [HttpGet]
    [SwaggerOperation("Get list of all users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers([FromQuery] GetAllUsers query)
        => Ok(await _getAllUsersHandler.HandlerAsync(query));
    
    [Authorize]
    [HttpGet("me")]
    [SwaggerOperation("The logged in user can view information about himself")]
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
    [SwaggerOperation("Displaying information about user based on ID")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    [SwaggerOperation("Create the user account")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUp(SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await _signUpHandler.HandlerAsync(command);
        
        await _emailService.SendEmailAsync(command.Email, "Welcome to our Dentica Dentistry!", "Thank you for signing up!");

        return NoContent();
    }

    [HttpPost("signIn")]
    [SwaggerOperation("Sign in the user and return the JSON Web Token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> SignIn(SignIn command)
    {
        await _signInHandler.HandlerAsync(command);
        var jwt = _tokenStorage.GetToken();
        return Ok(jwt);
    }
    
    [Authorize(Roles = "admin")]
    [HttpGet("{username}")]
    [SwaggerOperation("Displaying information about user based on username")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDto>> GetUserByName(string username)
    {
        var user = await _getUserByNameHandler.HandlerAsync(new GetUserByName { UserName = username });
        if (user is null)
        {
            return NotFound();
        }
        return user;
    }
}