using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;
using DenticaDentistry.Infrastructure.DAL;

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
}