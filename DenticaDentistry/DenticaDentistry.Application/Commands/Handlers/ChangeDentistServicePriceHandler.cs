using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Exceptions;
using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;

namespace DenticaDentistry.Application.Commands.Handlers;

public class ChangeDentistServicePriceHandler : ICommandHandler<ChangeDentistServicePrice>
{
    private readonly IDentistIndustryRepository _dentistIndustryRepository;

    public ChangeDentistServicePriceHandler(IDentistIndustryRepository dentistIndustryRepository)
    {
        _dentistIndustryRepository = dentistIndustryRepository;
    }

    public async Task HandlerAsync(ChangeDentistServicePrice command)
    {
        var dentistService = await GetDentistServicesAsync(command.DentistIndustryId);
        if (dentistService is null)
        {
            throw new DentistIndustryIdNotFoundException();
        }
        
        dentistService.ChangePrice(command.Price);
        await _dentistIndustryRepository.UpdateServiceAsync(dentistService);
    }
    
    private async Task<DentistIndustry> GetDentistServicesAsync(int id)
    {
        var services = await _dentistIndustryRepository.GetAllServiceAsync();

        return services.SingleOrDefault(x => x.DentistIndustryId == id);
    }
}