using DenticaDentistry.Core.Entities;

namespace DenticaDentistry.Core.Repositories;

public interface IServiceTypeRepository
{
    Task AddServiceTypeAsync(ServiceType serviceType);
}