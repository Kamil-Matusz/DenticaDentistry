using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record AddLicenseNumber(Guid DentistId, string LicenseNumber) : ICommand;