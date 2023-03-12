using Dentica_Dentistry.Application.Commands;
using Dentica_Dentistry.Application.DTO;
using Dentica_Dentistry.Core.Entities;
using Dentica_Dentistry.Core.Repositories;

namespace Dentica_Dentistry.Application.Services;

public class DentistsService : IDentistsService
{
    private readonly IDentistIndustryRepository _dentistIndustryRepository;

    public DentistsService(IDentistIndustryRepository dentistIndustryRepository)
    {
        _dentistIndustryRepository = dentistIndustryRepository;
    }
    public async Task<IEnumerable<DentistIndustryDto>> GetAllServicesAsync()
    {
        var service = await _dentistIndustryRepository.GetAllServiceAsync();

        return service
            .Select(x => new DentistIndustryDto
            {
                DentistIndustryId = x.DentistIndustryId,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description
            });
    }

    public async Task<DentistIndustryDto> GetServiceAsync(int id)
    {
        var service = await GetAllServicesAsync();
        return service.SingleOrDefault(x => x.DentistIndustryId == id);
    }

    public async Task<int?> CreateDentistServiceAsync(CreateDentistService command)
    {
        var dentistIndustryService = new DentistIndustry(command.DentistIndustryId, command.Name, command.Price, command.Description);
        await _dentistIndustryRepository.AddServiceAsync(dentistIndustryService);
        return dentistIndustryService.DentistIndustryId;
    }

    public async Task<bool> UpdateDentistServiceName(ChangeDentistServiceName command)
    {
        var dentistService = await GetDentistServicesAsync(command.DentistIndustryId);
        if (dentistService is null)
        {
            return false;
        }
        
        dentistService.ChangeName(command.Name);
        await _dentistIndustryRepository.UpdateServiceAsync(dentistService);
        return true;
    }

    public async Task<bool> UpdateDentistServicePrice(ChangeDentistServicePrice command)
    {
        var dentistService = await GetDentistServicesAsync(command.DentistIndustryId);
        if (dentistService is null)
        {
            return false;
        }
        
        dentistService.ChangePrice(command.Price);
        await _dentistIndustryRepository.UpdateServiceAsync(dentistService);
        return true;
    }

    public async Task<bool> DeleteServiceAsync(DeleteService command)
    {
        var dentistIndustryId = await GetDentistServicesAsync(command.DentistIndustryId);
        if (dentistIndustryId is null)
        {
            return false;
        }

        await _dentistIndustryRepository.DeleteServiceAsync(dentistIndustryId);
        return true;
    }

    private async Task<DentistIndustry> GetDentistServicesAsync(int id)
    {
        var services = await _dentistIndustryRepository.GetAllServiceAsync();

        return services.SingleOrDefault(x => x.DentistIndustryId == id);
    }
}