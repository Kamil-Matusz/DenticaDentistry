using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record CreateDentist(Guid DentistId,Guid UserId) : ICommand;