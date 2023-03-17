using DenticaDentistry.Core.Entities;

namespace DenticaDentistry.Core.Repositories;

public interface IReservationRepository
{
    Task<IEnumerable<DentistIndustry>> GetAllReservationAsync();
    Task<DentistIndustry> GetReservationAsync(int id);
    Task AddAsync(DentistIndustry dentistIndustry);
    Task UpdateAsync(DentistIndustry dentistIndustry);
    Task DeleteAsync(DentistIndustry dentistIndustry);

}