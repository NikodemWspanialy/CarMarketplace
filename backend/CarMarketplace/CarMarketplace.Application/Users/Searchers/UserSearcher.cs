using CarMarketplace.Application.Users.Exceptions;
using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Domain.Users;

namespace CarMarketplace.Application.Users.Searchers;

internal interface IUserSearcher
{
    Task<User> FindByIdAsync(Guid id, CancellationToken token = default);
}

internal class UserSearcher(IUserRepository userRepository) : IUserSearcher
{
    public async Task<User> FindByIdAsync(Guid id, CancellationToken token = default) =>
        await userRepository.GetUserByIdAsync(id, token)
            ?? throw new UserNotFound(id);
}
