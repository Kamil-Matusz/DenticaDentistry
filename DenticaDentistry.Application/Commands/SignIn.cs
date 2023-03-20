using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record SignIn(string Email, string Password) : ICommand;