using Dentica_Dentistry.Core.Entities;

namespace Dentica_Dentistry.Core.Repositories;

public interface IDentistIndustryRepository
{
    Task<IEnumerable<DentistIndustry>> GetAllServiceAsync();
    Task<DentistIndustry> GetServiceAsync(int id);
}