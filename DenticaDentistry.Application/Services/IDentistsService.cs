using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;

namespace DenticaDentistry.Application.Services;

public interface IDentistsService
{
    Task<IEnumerable<DentistIndustryDto>> GetAllServicesAsync();
    Task<DentistIndustryDto> GetServiceAsync(int id);
    Task<int?> CreateDentistServiceAsync(CreateDentistService command);
    Task<bool> UpdateDentistServiceName(ChangeDentistServiceName command);
    Task<bool> UpdateDentistServicePrice(ChangeDentistServicePrice command);
    Task<bool> DeleteServiceAsync(DeleteDentistService command);
}