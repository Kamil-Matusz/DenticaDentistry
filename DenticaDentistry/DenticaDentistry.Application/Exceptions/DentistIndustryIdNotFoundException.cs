using DenticaDentistry.Core.Exceptions;

namespace DenticaDentistry.Application.Exceptions;

public sealed class DentistIndustryIdNotFoundException : CustomException
{
    public DentistIndustryIdNotFoundException() : base("Dentist Industry with ID was not found.")
    {
    }
}