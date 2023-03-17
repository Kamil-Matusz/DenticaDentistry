using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;

namespace DenticaDentistry.Application.Commands.Handlers;

public class CreateDentistServiceHandler : ICommandHandler<CreateDentistService>
{
    private readonly IDentistIndustryRepository _dentistIndustryRepository;

    public CreateDentistServiceHandler(IDentistIndustryRepository dentistIndustryRepository)
    {
        _dentistIndustryRepository = dentistIndustryRepository;
    }

    public async Task HandlerAsync(CreateDentistService command)
    {
        var dentistIndustryService = new DentistIndustry(command.DentistIndustryId, command.Name, command.Price, command.Description);
        await _dentistIndustryRepository.AddServiceAsync(dentistIndustryService);
    }
}