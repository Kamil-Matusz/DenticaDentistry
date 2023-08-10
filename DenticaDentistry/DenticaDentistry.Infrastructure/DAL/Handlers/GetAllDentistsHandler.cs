using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Handlers;

internal sealed class GetAllDentistsHandler : IQueryHandler<GetAllDentists,IEnumerable<DentistDto>>
{
    private readonly DenticaDentistryDbContext _dbContext;

    public GetAllDentistsHandler(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<DentistDto>> HandlerAsync(GetAllDentists query)
    {
        var dentists = await _dbContext.Dentists
            .Join(
                _dbContext.Users,
                dentist => dentist.UserId,
                user => user.UserId,
                (dentist, user) => new DentistDto
                {
                    DentistId = dentist.DentistId,
                    Email = user.Email,
                    Fullname = user.Fullname,
                    PhoneNumber = user.PhoneNumber,
                }
            )
            .ToListAsync();

        return dentists;
    }
}