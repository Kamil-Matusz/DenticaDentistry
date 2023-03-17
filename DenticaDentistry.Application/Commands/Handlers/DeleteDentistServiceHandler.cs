using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Exceptions;
using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;

namespace DenticaDentistry.Application.Commands.Handlers;

public class DeleteDentistServiceHandler : ICommandHandler<DeleteDentistService>
{
    private readonly IDentistIndustryRepository _dentistIndustryRepository;

    public DeleteDentistServiceHandler(IDentistIndustryRepository dentistIndustryRepository)
    {
        _dentistIndustryRepository = dentistIndustryRepository;
    }

    public async Task HandlerAsync(DeleteDentistService command)
    {
        var dentistIndustryId = await GetDentistServicesAsync(command.DentistIndustryId);
        if (dentistIndustryId is null)
        {
            throw new DentistIndustryIdNotFoundException();
        }

        await _dentistIndustryRepository.DeleteServiceAsync(dentistIndustryId);
    }
    
    private async Task<DentistIndustry> GetDentistServicesAsync(int id)
    {
        var services = await _dentistIndustryRepository.GetAllServiceAsync();

        return services.SingleOrDefault(x => x.DentistIndustryId == id);
    }
}