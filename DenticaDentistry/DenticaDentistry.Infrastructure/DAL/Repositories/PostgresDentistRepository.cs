using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;
using DenticaDentistry.Core.ValueObjects;
using DenticaDentistry.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Repositories;

internal sealed class PostgresDentistRepository : IDentistRepository
{
    private readonly DenticaDentistryDbContext _dbContext;

    public PostgresDentistRepository(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Dentist dentist)
    {
        await _dbContext.AddAsync(dentist);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Dentist dentist)
    {
        _dbContext.Update(dentist);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Dentist> GetDentistById(DentistId id) => await _dbContext.Dentists.FirstOrDefaultAsync(x => x.DentistId == id);
}