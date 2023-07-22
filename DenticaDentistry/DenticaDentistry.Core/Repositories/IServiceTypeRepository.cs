using DenticaDentistry.Core.Entities;

namespace DenticaDentistry.Core.Repositories;

public interface IServiceTypeRepository
{
    Task<IEnumerable<ServiceType>> GetAllServiceTypeAsync();
    Task AddServiceTypeAsync(ServiceType serviceType);
    Task UpdateServiceTypeAsync(ServiceType serviceType);
}