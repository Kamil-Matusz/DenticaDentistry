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

    public IEnumerable<DentistIndustry> GetAllReservation() => _dbContext.DentistIndustries
        .Include(x => x.Reservations)
        .ToList();

    public DentistIndustry GetReservation(int id) =>
        _dbContext.DentistIndustries
            .Include(x => x.Reservations)
            .SingleOrDefault(x => x.DentistIndustryId == id);

    public void Add(DentistIndustry dentistIndustry)
    {
        _dbContext.Add(dentistIndustry);
        _dbContext.SaveChanges();
    }

    public void Update(DentistIndustry dentistIndustry)
    {
        _dbContext.Update(dentistIndustry);
        _dbContext.SaveChanges();
    }

    public void Delete(DentistIndustry dentistIndustry)
    {
        _dbContext.Remove(dentistIndustry);
        _dbContext.SaveChanges();
    }
}