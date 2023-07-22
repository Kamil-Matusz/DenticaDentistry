using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Infrastructure.DAL;
using DenticaDentistry.Infrastructure.DAL.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Handlers;

internal sealed class GetDentistServiceHandler : IQueryHandler<GetDentistService,DentistIndustryDto>
{
    private readonly DenticaDentistryDbContext _dbContext;

    public GetDentistServiceHandler(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DentistIndustryDto> HandlerAsync(GetDentistService query)
    {
        var dentistServiceId = query.DentistServiceId;
        var service = await _dbContext.DentistIndustries
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.DentistIndustryId == dentistServiceId);

        return service.AsDentistIndustryDto();
    }
}