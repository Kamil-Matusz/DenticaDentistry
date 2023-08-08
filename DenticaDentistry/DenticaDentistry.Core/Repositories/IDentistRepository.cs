using DenticaDentistry.Core.Entities;

namespace DenticaDentistry.Core.Repositories;

public interface IDentistRepository
{
    Task AddAsync(Dentist dentist);
}