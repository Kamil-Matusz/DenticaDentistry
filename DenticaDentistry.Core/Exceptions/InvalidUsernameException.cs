namespace DenticaDentistry.Core.Exceptions;

public sealed class InvalidUsernameException : CustomException
{
    public string Username { get; }

    public InvalidUsernameException(string username) : base($"Full name: '{username}' is invalid.")
    {
        Username = username;
    }
}