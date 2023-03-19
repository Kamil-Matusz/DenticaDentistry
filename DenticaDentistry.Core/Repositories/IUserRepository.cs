using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.ValueObjects;

namespace DenticaDentistry.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id);
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetByUsernameAsync(Username username);
    Task AddAsync(User user);
}