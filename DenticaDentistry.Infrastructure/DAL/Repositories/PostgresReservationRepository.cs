using Dentica_Dentistry.Core.Entities;
using Dentica_Dentistry.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Repositories;

internal sealed class PostgresReservationRepository : IReservationRepository
{
    private readonly DenticaDentistryDbContext _dbContext;

    public PostgresReservationRepository(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<DentistIndustry>> GetAllReservationAsync()
    {
       var result = await _dbContext.DentistIndustries
            .Include(x => x.Reservations)
            .ToListAsync();

       return result.AsEnumerable();
    }

    public Task<DentistIndustry> GetReservationAsync(int id) =>
        _dbContext.DentistIndustries
            .Include(x => x.Reservations)
            .SingleOrDefaultAsync(x => x.DentistIndustryId == id);
    
    public async Task AddAsync(DentistIndustry dentistIndustry)
    {
        await _dbContext.AddAsync(dentistIndustry);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(DentistIndustry dentistIndustry)
    {
        _dbContext.Update(dentistIndustry);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(DentistIndustry dentistIndustry)
    {
        _dbContext.Remove(dentistIndustry);
        await _dbContext.SaveChangesAsync();
    }
}