using DenticaDentistry.Core.Exceptions;

namespace DenticaDentistry.Application.Exceptions;

public sealed class EmailAlreadyInUseException : CustomException
{
    public string Email { get; }

    public EmailAlreadyInUseException(string email) : base($"Email: '{email}' is already in use.")
    {
        Email = email;
    }
}