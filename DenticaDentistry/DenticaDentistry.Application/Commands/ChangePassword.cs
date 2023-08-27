using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record ChangePassword(Guid UserId, string NewPassword) : ICommand;