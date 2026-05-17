using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Domain.Users;
using CarMarketplace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarMarketplace.Infrastructure.Repositories;

public class UserRepository(CarMarketplaceDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken token = default) =>
        await dbContext.Users
            .Include(u => u.BanHistory)
            .FirstOrDefaultAsync(u => u.Id == userId, token);

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken token = default) =>
        await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, token);

    public async Task AddUserAsync(User user, CancellationToken token = default) =>
        await dbContext.Users.AddAsync(user, cancellationToken: token);

    public Task UpdateUserAsync(User user, CancellationToken token = default)
    {
        dbContext.Users.Update(user);

        return Task.CompletedTask;
    }

    public async Task<(IReadOnlyList<User> Users, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, CancellationToken token = default)
    {
        var query = dbContext.Users.AsNoTracking();
        var totalCount = await query.CountAsync(token);

        var users = await query
            .OrderBy(u => u.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);

        return (users, totalCount);
    }
}