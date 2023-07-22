using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record ChangeServiceTypeName(int ServiceTypeId,string name) : ICommand;