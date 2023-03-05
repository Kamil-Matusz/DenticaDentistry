using Dentica_Dentistry.Core.Entities;

namespace Dentica_Dentistry.Application.Repositories;

public interface IDentistIndustryRepository
{
    IEnumerable<DentistIndustry> GetAllReservation();
    DentistIndustry GetReservation(int id);
    void Add(DentistIndustry dentistIndustry);
    void Update(DentistIndustry dentistIndustry);
    void Delete(DentistIndustry dentistIndustry);

}