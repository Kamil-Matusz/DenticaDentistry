using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record ChangeDentistServicePrice(int DentistIndustryId, double Price) : ICommand;
  