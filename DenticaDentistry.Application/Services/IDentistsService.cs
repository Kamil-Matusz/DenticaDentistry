using Dentica_Dentistry.Application.Commands;
using Dentica_Dentistry.Application.DTO;

namespace Dentica_Dentistry.Application.Services;

public interface IDentistsService
{
    Task<IEnumerable<DentistIndustryDto>> GetAllServicesAsync();
    Task<DentistIndustryDto> GetServiceAsync(int id);
    Task<int?> CreateDentistServiceAsync(CreateDentistService command);
    Task<bool> UpdateDentistServiceName(ChangeDentistServiceName command);
    Task<bool> UpdateDentistServicePrice(ChangeDentistServicePrice command);
    Task<bool> DeleteServiceAsync(DeleteService command);
}