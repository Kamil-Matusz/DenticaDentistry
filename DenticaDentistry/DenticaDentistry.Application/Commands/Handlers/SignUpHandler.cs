using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Exceptions;
using DenticaDentistry.Application.Security;
using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;
using DenticaDentistry.Core.ValueObjects;

namespace DenticaDentistry.Application.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IPasswordManager _passwordManager;
    private readonly IUserRepository _userRepository;

    public SignUpHandler(IPasswordManager passwordManager, IUserRepository userRepository)
    {
        _passwordManager = passwordManager;
        _userRepository = userRepository;
    }

    public async Task HandlerAsync(SignUp command)
    {
        var userId = new UserId(command.UserId);
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(command.Password);
        var phoneNumber = new PhoneNumber(command.PhoneNumber);
        var fullName = new Fullname(command.Fullname);
        var role = string.IsNullOrWhiteSpace(command.Role) ? Role.User() : new Role(command.Role);
        
        if (await _userRepository.GetByEmailAsync(email) is not null)
        {
            throw new EmailAlreadyInUseException(email);
        }

        if (await _userRepository.GetByUsernameAsync(username) is not null)
        {
            throw new UsernameAlreadyInUseException(username);
        }
        
        var securedPassword = _passwordManager.Secure(password);
        var user = new User(userId, email, username, securedPassword, fullName, role, phoneNumber);
        
        await _userRepository.AddAsync(user);
    }
}