using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Exceptions;
using DenticaDentistry.Application.Security;
using DenticaDentistry.Core.Repositories;
using DenticaDentistry.Core.ValueObjects;

namespace DenticaDentistry.Application.Commands.Handlers;

internal sealed class ChangePasswordHandler : ICommandHandler<ChangePassword>
{
    private readonly IPasswordManager _passwordManager;
    private readonly IUserRepository _userRepository;

    public ChangePasswordHandler(IPasswordManager passwordManager, IUserRepository userRepository)
    {
        _passwordManager = passwordManager;
        _userRepository = userRepository;
    }

    public async Task HandlerAsync(ChangePassword command)
    {
        var userId = new UserId(command.UserId);
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException(userId);
        }
        
        var newSecuredPassword = _passwordManager.Secure(command.NewPassword);
        user.ChangePassword(newSecuredPassword);

        await _userRepository.UpdateAsync(user);

    }
}