using DenticaDentistry.Core.ValueObjects;

namespace DenticaDentistry.Core.Entities;

public class Dentist
{
    public DentistId DentistId { get; private set; }
    public UserId UserId { get; private set; }
    public string LicenseNumber { get; private set; }

}