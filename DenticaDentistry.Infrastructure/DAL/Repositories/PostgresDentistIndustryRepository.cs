using Dentica_Dentistry.Core.Entities;
using Dentica_Dentistry.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Repositories;

internal sealed class PostgresDentistIndustryRepository  : IDentistIndustryRepository
{
    private readonly DenticaDentistryDbContext _dbContext;

    public PostgresDentistIndustryRepository(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<DentistIndustry>> GetAllServiceAsync()
    {
        var result = await _dbContext.DentistIndustries.ToListAsync();

        return result.AsEnumerable();
    }

    public Task<DentistIndustry> GetServiceAsync(int id) => _dbContext.DentistIndustries.SingleOrDefaultAsync(x => x.DentistIndustryId == id);
    public async Task AddServiceAsync(DentistIndustry dentistIndustry)
    {
        await _dbContext.AddAsync(dentistIndustry);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateServiceAsync(DentistIndustry dentistIndustry)
    {
         _dbContext.Update(dentistIndustry);
         await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteServiceAsync(DentistIndustry dentistIndustry)
    {
        _dbContext.Remove(dentistIndustry);
        await _dbContext.SaveChangesAsync();
    }
}