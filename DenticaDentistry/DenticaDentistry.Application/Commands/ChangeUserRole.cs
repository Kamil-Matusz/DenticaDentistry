using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record ChangeUserRole(Guid UserId, string Role) : ICommand;