using DenticaDentistry.Core.Exceptions;

namespace DenticaDentistry.Application.Exceptions;

public class UserRoleNotExistException : CustomException
{
    public UserRoleNotExistException() : base("This role don't exist")
    {
    }
}