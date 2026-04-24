using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Application.Users.Searchers;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.DowngradeToUser;

internal class DowngradeToUserHandler(
    IUserSearcher userSearcher,
    IUserRepository userRepository) : IRequestHandler<DowngradeToUserRequest, Unit>
{
    public async Task<Unit> Handle(DowngradeToUserRequest request, CancellationToken token)
    {
        var user = await userSearcher.FindByIdAsync(request.UserId, token);

        user.DemoteToUser();

        await userRepository.UpdateUserAsync(user, token);

        return Unit.Value;
    }
}
