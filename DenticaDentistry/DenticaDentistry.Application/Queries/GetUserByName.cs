using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;

namespace DenticaDentistry.Application.Queries;

public class GetUserByName : IQuery<UserDto>
{
    public string UserName { get; set; }
}