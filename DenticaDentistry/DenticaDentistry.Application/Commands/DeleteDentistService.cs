using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record DeleteDentistService(int DentistIndustryId) : ICommand;