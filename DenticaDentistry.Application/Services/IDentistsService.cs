using Dentica_Dentistry.Application.DTO;

namespace Dentica_Dentistry.Application.Services;

public interface IDentistsService
{
    Task<IEnumerable<DentistIndustryDto>> GetAllServicesAsync();
    Task<DentistIndustryDto> GetServiceAsync(int id);
}