using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;
using DenticaDentistry.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Repositories;

internal sealed class PostgresServiceTypeRepository : IServiceTypeRepository
{
    private readonly DenticaDentistryDbContext _dbContext;

    public PostgresServiceTypeRepository(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ServiceType>> GetAllServiceTypeAsync()
    {
        var result = await _dbContext.ServiceTypes
            .AsNoTracking()
            .ToListAsync();

        return result.AsEnumerable();
    }

    public async Task AddServiceTypeAsync(ServiceType serviceType)
    {
        await _dbContext.AddAsync(serviceType);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateServiceTypeAsync(ServiceType serviceType)
    {
        _dbContext.Update(serviceType);
        await _dbContext.SaveChangesAsync();
    }
}