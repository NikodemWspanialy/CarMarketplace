using CarMarketplace.Application.Admin.Exceptions;
using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Application.Users.Searchers;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.DowngradeToUser;

internal class DowngradeToUserHandler(
    ICurrentUserProvider currentUserProvider,
    IUserSearcher userSearcher,
    IUserRepository userRepository) : IRequestHandler<DowngradeToUserRequest, Unit>
{
    public async Task<Unit> Handle(DowngradeToUserRequest request, CancellationToken token)
    {
        var currentUserId = currentUserProvider.GetUserId();

        if (request.UserId == currentUserId)
            throw new CannotDemoteYourself();

        var user = await userSearcher.FindByIdAsync(request.UserId, token);

        user.DemoteToUser();

        await userRepository.UpdateUserAsync(user, token);

        return Unit.Value;
    }
}
