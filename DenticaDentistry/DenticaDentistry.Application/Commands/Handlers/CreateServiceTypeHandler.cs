using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;

namespace DenticaDentistry.Application.Commands.Handlers;

public class CreateServiceTypeHandler : ICommandHandler<CreateServiceType>
{
    private readonly IServiceTypeRepository _serviceTypeRepository;

    public CreateServiceTypeHandler(IServiceTypeRepository serviceTypeRepository)
    {
        _serviceTypeRepository = serviceTypeRepository;
    }

    public async Task HandlerAsync(CreateServiceType command)
    {
        var serviceType = new ServiceType(command.ServiceTypeId, command.Name);
        await _serviceTypeRepository.AddServiceTypeAsync(serviceType);
    }
}