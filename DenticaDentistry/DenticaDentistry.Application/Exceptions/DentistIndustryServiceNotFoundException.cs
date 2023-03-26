using DenticaDentistry.Core.Exceptions;

namespace DenticaDentistry.Application.Exceptions;

public sealed class DentistIndustryServiceNotFoundException : CustomException
{
    public int? Id { get;  }
    public DentistIndustryServiceNotFoundException(int id) : base($"Dentist Industry wit ID: {id} was not found")
    {
        Id = id;
    }
}