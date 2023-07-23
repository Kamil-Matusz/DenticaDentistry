using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Infrastructure.DAL;
using DenticaDentistry.Infrastructure.DAL.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Handlers;

internal sealed class GetUserByNameHandler : IQueryHandler<GetUserByName,UserDto>
{
    private readonly DenticaDentistryDbContext _dbContext;

    public GetUserByNameHandler(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto> HandlerAsync(GetUserByName query)
    {
        var userName = query.UserName;
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Username == userName);

        return user.AsUsersDto();
    }
}