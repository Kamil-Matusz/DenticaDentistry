using Dentica_Dentistry.Core.Entities;

namespace Dentica_Dentistry.Core.Repositories;

public interface IReservationRepository
{
    IEnumerable<DentistIndustry> GetAllReservation();
    DentistIndustry GetReservation(int id);
    void Add(DentistIndustry dentistIndustry);
    void Update(DentistIndustry dentistIndustry);
    void Delete(DentistIndustry dentistIndustry);

}