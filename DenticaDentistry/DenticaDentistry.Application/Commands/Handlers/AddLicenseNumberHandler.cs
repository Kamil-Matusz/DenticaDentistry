using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Exceptions;
using DenticaDentistry.Core.Repositories;

namespace DenticaDentistry.Application.Commands.Handlers;

public sealed class AddLicenseNumberHandler : ICommandHandler<AddLicenseNumber>
{
    private readonly IDentistRepository _dentistRepository;

    public AddLicenseNumberHandler(IDentistRepository dentistRepository)
    {
        _dentistRepository = dentistRepository;
    }

    public async Task HandlerAsync(AddLicenseNumber command)
    {
        var dentist = await _dentistRepository.GetDentistById(command.DentistId);
        if (dentist is null)
        {
            throw new DentistIdNotFoundException();
        }
        
        dentist.AddLicenseNumber(command.LicenseNumber);
        await _dentistRepository.UpdateAsync(dentist);
    }
}