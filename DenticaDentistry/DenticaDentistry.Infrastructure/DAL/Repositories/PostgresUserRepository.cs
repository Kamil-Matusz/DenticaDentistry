using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;
using DenticaDentistry.Core.ValueObjects;
using DenticaDentistry.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Repositories;

internal sealed class PostgresUserRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    private readonly DenticaDentistryDbContext _dbContext;

    public PostgresUserRepository(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
        _users = dbContext.Users;
    }

    public Task<User> GetByIdAsync(UserId id) => _users.SingleOrDefaultAsync(x => x.UserId == id);

    public Task<User> GetByEmailAsync(Email email) => _users.SingleOrDefaultAsync(x => x.Email == email);

    public Task<User> GetByUsernameAsync(Username username) => _users.SingleOrDefaultAsync(x => x.Username == username);

    public async Task AddAsync(User user) => await _users.AddAsync(user);
    public async Task ChangeUserRole(UserId userId, Role role)
    {
        var user = await _users.SingleOrDefaultAsync(x => x.UserId == userId);

        user.Role = role;
        await _dbContext.SaveChangesAsync();
    }
}