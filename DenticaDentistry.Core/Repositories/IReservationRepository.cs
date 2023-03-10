using Dentica_Dentistry.Core.Entities;

namespace Dentica_Dentistry.Core.Repositories;

public interface IReservationRepository
{
    Task<IEnumerable<DentistIndustry>> GetAllReservationAsync();
    Task<DentistIndustry> GetReservationAsync(int id);
    Task AddAsync(DentistIndustry dentistIndustry);
    Task UpdateAsync(DentistIndustry dentistIndustry);
    Task DeleteAsync(DentistIndustry dentistIndustry);

}