using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Dentica_Dentistry.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly ICommandHandler<SignUp> _signUpHandler;
    private readonly IQueryHandler<GetAllUsers, IEnumerable<UserDto>> _getAllUsersHandler;
    private readonly IQueryHandler<GetUser, UserDto> _getUserHandler;
    
    public UsersController(ICommandHandler<SignUp> signUpHandler, IQueryHandler<GetAllUsers, IEnumerable<UserDto>> getAllUsersHandler, IQueryHandler<GetUser, UserDto> getUserHandler)
    {
        _signUpHandler = signUpHandler;
        _getAllUsersHandler = getAllUsersHandler;
        _getUserHandler = getUserHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers([FromQuery] GetAllUsers query)
        => Ok(await _getAllUsersHandler.HandlerAsync(query));

    [HttpPost]
    public async Task<ActionResult> CreateAccount(SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await _signUpHandler.HandlerAsync(command);

        return NoContent();
    }
}