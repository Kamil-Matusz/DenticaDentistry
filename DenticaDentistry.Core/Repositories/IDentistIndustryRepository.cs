using DenticaDentistry.Core.Entities;

namespace DenticaDentistry.Core.Repositories;

public interface IDentistIndustryRepository
{
    Task<IEnumerable<DentistIndustry>> GetAllServiceAsync();
    Task<DentistIndustry> GetServiceAsync(int id);
    Task AddServiceAsync(DentistIndustry dentistIndustry);
    Task UpdateServiceAsync(DentistIndustry dentistIndustry);
    Task DeleteServiceAsync(DentistIndustry dentistIndustry);
}