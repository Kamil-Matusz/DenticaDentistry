using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Exceptions;
using DenticaDentistry.Core.Repositories;
using DenticaDentistry.Core.ValueObjects;

namespace DenticaDentistry.Application.Commands.Handlers;

public sealed class ChangeUserRoleHandler : ICommandHandler<ChangeUserRole>
{
    private readonly IUserRepository _userRepository;

    public ChangeUserRoleHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task HandlerAsync(ChangeUserRole command)
    {
        var userId = command.UserId;
        var role = string.IsNullOrWhiteSpace(command.Role) ? Role.User() : new Role(command.Role);
        if (role == "admin" || role == "user" || role == "dentist")
        {
            await _userRepository.ChangeUserRole(userId, role);    
        }
        else
        {
            throw new UserRoleNotExistException();
        }
    }
}