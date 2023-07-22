using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Exceptions;
using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;

namespace DenticaDentistry.Application.Commands.Handlers;

public class ChangeServiceTypeNameHandler : ICommandHandler<ChangeServiceTypeName>
{
    private readonly IServiceTypeRepository _serviceTypeRepository;

    public ChangeServiceTypeNameHandler(IServiceTypeRepository serviceTypeRepository)
    {
        _serviceTypeRepository = serviceTypeRepository;
    }

    public async Task HandlerAsync(ChangeServiceTypeName command)
    {
        var serviceType = await GetServiceType(command.ServiceTypeId);
        if (serviceType is null)
        {
            throw new DentistIndustryIdNotFoundException();
        }
        
        serviceType.ChangeServiceName(command.name);
        await _serviceTypeRepository.UpdateServiceTypeAsync(serviceType);
    }

    private async Task<ServiceType> GetServiceType(int id)
    {
        var serviceTypes = await _serviceTypeRepository.GetAllServiceTypeAsync();
        return serviceTypes.SingleOrDefault(x => x.ServiceTypeId == id);
    }
}