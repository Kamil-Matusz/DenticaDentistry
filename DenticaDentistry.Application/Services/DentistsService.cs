using Dentica_Dentistry.Application.DTO;
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
}