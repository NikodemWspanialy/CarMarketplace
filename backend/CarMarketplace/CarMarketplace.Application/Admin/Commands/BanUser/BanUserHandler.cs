using CarMarketplace.Application.Admin.Exceptions;
using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Application.Users.Searchers;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.BanUser;

internal class BanUserHandler(
    ICurrentUserProvider currentUserProvider,
    IUserSearcher userSearcher,
    IUserRepository userRepository) : IRequestHandler<BanUserRequest, Unit>
{
    public async Task<Unit> Handle(BanUserRequest request, CancellationToken token)
    {
        var adminId = currentUserProvider.GetUserId();

        if (request.UserId == adminId)
            throw new CannotBanYourself();

        var user = await userSearcher.FindByIdAsync(request.UserId, token);

        user.Ban(request.Reason, adminId, request.ExpiresAt);
        await userRepository.UpdateUserAsync(user, token);

        return Unit.Value;
    }
}
