using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;
using DenticaDentistry.Core.ValueObjects;

namespace DenticaDentistry.Application.Commands.Handlers;

internal sealed class CreateDentistHandler : ICommandHandler<CreateDentist>
{
    private readonly IDentistRepository _dentistRepository;

    public CreateDentistHandler(IDentistRepository dentistRepository)
    {
        _dentistRepository = dentistRepository;
    }

    public async Task HandlerAsync(CreateDentist command)
    {
        var dentistId = new DentistId(command.DentistId);
        var userId = new UserId(command.UserId);

        var dentist = new Dentist(dentistId, userId);

        await _dentistRepository.AddAsync(dentist);
    }
}