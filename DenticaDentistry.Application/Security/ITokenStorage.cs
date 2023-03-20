using DenticaDentistry.Application.DTO;

namespace DenticaDentistry.Application.Security;

public interface ITokenStorage
{
    void SetToken(JwtDto jwt);
    JwtDto GetToken();
}