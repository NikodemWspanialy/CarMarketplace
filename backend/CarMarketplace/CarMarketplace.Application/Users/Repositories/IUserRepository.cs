using CarMarketplace.Domain.Users;

namespace CarMarketplace.Application.Users.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid userId, CancellationToken token = default);

    Task<User?> GetUserByEmailAsync(string email, CancellationToken token = default);

    Task AddUserAsync(User user, CancellationToken token = default);
}