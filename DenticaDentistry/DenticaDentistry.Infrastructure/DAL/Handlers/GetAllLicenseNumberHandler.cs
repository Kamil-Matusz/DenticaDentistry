using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Core.Models;
using DenticaDentistry.Infrastructure.DAL;
using DenticaDentistry.Infrastructure.DAL.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Handlers;

internal class GetAllLicenseNumberHandler : IQueryHandler<GetAllLicenseNumber, IEnumerable<DentistWithLicenseNumberDto>>
{
    private readonly DenticaDentistryDbContext _dbContext;

    public GetAllLicenseNumberHandler(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<DentistWithLicenseNumberDto>> HandlerAsync(GetAllLicenseNumber query)
    {
        var pager = new Pager(query.PageIndex, query.PageSize);
        var dentists = await _dbContext.Dentists
            .AsNoTracking()
            .Paginate(pager)
            .ToListAsync();

        return dentists.Select(x => x.AsDentistWithLicenseNumberDto());
    }
}