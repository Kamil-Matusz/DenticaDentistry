using DenticaDentistry.Core.ValueObjects;

namespace DenticaDentistry.Core.Entities;

public class Dentist
{
    public DentistId DentistId { get; private set; }
    public UserId UserId { get; private set; }
    public string LicenseNumber { get; private set; }

    public Dentist(DentistId dentistId, UserId userId, string licenseNumber)
    {
        DentistId = dentistId;
        UserId = userId;
        LicenseNumber = licenseNumber;
    }
    
    public Dentist(DentistId dentistId, UserId userId)
    {
        DentistId = dentistId;
        UserId = userId;
    }
    
    public void AddLicenseNumber(string licenseNumber)
    {
        LicenseNumber = licenseNumber;
    }
}