using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.ValueObjects;

namespace DenticaDentistry.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id);
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetByUsernameAsync(Username username);
    Task AddAsync(User user);
    Task ChangeUserRole(UserId userId, Role role);
    Task UpdateAsync(User user);
    Task<string> GetUserEmail(UserId id);
}