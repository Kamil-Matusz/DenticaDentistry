using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;
using DenticaDentistry.Infrastructure.DAL;

namespace Dentica_Dentistry.Infrastructure.DAL.Repositories;

internal sealed class PostgresServiceTypeRepository : IServiceTypeRepository
{
    private readonly DenticaDentistryDbContext _dbContext;

    public PostgresServiceTypeRepository(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddServiceTypeAsync(ServiceType serviceType)
    {
        await _dbContext.AddAsync(serviceType);
        await _dbContext.SaveChangesAsync();
    }
}