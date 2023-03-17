using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Infrastructure.DAL;
using DenticaDentistry.Infrastructure.DAL.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Handlers;

internal class GetAllDentistServicesHandler : IQueryHandler<GetAllDentistServices, IEnumerable<DentistIndustryDto>>
{
    private readonly DenticaDentistryDbContext _dbContext;

    public GetAllDentistServicesHandler(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<DentistIndustryDto>> HandlerAsync(GetAllDentistServices query)
    {
        var services = await _dbContext.DentistIndustries
            .AsNoTracking()
            .ToListAsync();

        return services.Select(x => x.AsDentistIndustryDto());
    }
}