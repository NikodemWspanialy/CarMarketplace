using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Domain.Users;
using CarMarketplace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarMarketplace.Infrastructure.Repositories;

public class UserRepository(CarMarketplaceDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken token = default) =>
        await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, token);

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken token = default) =>
        await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, token);

    public async Task AddUserAsync(User user, CancellationToken token = default)
    {
        await dbContext.Users.AddAsync(user, cancellationToken: token);
    }
}