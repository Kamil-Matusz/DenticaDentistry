using DenticaDentistry.Core.Exceptions;

namespace DenticaDentistry.Application.Exceptions;

public class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException(string message) : base("Invalid credentials.")
    {
    }
}