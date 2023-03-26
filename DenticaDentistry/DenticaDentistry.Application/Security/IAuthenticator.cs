using DenticaDentistry.Application.DTO;

namespace DenticaDentistry.Application.Security;

public interface IAuthenticator
{
   JwtDto CreateToken(Guid userId, string role);
}