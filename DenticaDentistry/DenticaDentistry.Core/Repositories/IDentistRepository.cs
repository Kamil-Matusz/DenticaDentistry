using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.ValueObjects;

namespace DenticaDentistry.Core.Repositories;

public interface IDentistRepository
{
    Task AddAsync(Dentist dentist);
    Task UpdateAsync(Dentist dentist);
    Task<Dentist> GetDentistById(DentistId id);
}