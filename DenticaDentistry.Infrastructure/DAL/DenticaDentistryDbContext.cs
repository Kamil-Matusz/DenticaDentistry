using DenticaDentistry.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DenticaDentistry.Infrastructure.DAL;

internal class DenticaDentistryDbContext : DbContext
{
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<DentistIndustry> DentistIndustries { get; set; }

    public DenticaDentistryDbContext(DbContextOptions<DenticaDentistryDbContext> options) : base(options) 
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties().Where(p => p.IsPrimaryKey()))
            {
                property.ValueGenerated = ValueGenerated.Never;
            }
        }
    }
}