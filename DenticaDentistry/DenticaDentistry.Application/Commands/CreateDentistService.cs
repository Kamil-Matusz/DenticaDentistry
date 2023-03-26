using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record CreateDentistService(int DentistIndustryId, string Name, double Price, string Description) : ICommand;
