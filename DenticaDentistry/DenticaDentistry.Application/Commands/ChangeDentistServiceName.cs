using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record ChangeDentistServiceName(int DentistIndustryId,string Name) : ICommand;