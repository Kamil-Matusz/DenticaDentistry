using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Infrastructure.DAL;
using DenticaDentistry.Infrastructure.DAL.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Handlers;

internal class GetAllUsersHandler : IQueryHandler<GetAllUsers, IEnumerable<UserDto>>
{
    private readonly DenticaDentistryDbContext _dbContext;

    public GetAllUsersHandler(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserDto>> HandlerAsync(GetAllUsers query)
    {
        var users = await _dbContext.Users
            .AsNoTracking()
            .Select(x => x.AsUsersDto())
            .ToListAsync();

        return users;
    }
}