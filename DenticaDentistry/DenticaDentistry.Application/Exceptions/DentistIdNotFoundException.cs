using DenticaDentistry.Core.Exceptions;

namespace DenticaDentistry.Application.Exceptions;

public sealed class DentistIdNotFoundException : CustomException
{
    public DentistIdNotFoundException() : base("Dentist with ID was not found.")
    {
    }
}