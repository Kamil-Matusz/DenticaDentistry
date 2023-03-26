using DenticaDentistry.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace DenticaDentistry.IntegrationTests;

internal sealed class TestDatabase : IDisposable
{
    public DenticaDentistryDbContext DbContext { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<DatabaseOptions>("database");
        DbContext = new DenticaDentistryDbContext(new DbContextOptionsBuilder<DenticaDentistryDbContext>().UseNpgsql(options.connectionString).Options);
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}