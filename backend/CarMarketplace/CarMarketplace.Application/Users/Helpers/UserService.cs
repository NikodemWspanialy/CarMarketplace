using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Application.Users.Searchers;

namespace CarMarketplace.Application.Users.Helpers;

internal interface IUserService
{
    Task DeleteAsync(Guid userId, CancellationToken token = default);
}

internal class UserService(
    IUserSearcher userSearcher,
    IUserRepository userRepository) : IUserService
{
    public async Task DeleteAsync(Guid userId, CancellationToken token = default)
    {
        var user = await userSearcher.FindByIdAsync(userId, token);

        user.Delete();
        await userRepository.UpdateUserAsync(user, token);
    }
}
