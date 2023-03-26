using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record SignUp(Guid UserId, string Email, string Username, string Password, string Fullname, string Role) : ICommand;
