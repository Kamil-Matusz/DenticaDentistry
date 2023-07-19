using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record CreateServiceType(int ServiceTypeId, string Name): ICommand;