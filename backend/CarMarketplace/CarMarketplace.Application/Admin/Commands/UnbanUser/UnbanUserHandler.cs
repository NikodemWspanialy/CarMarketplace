using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Application.Users.Searchers;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.UnbanUser;

internal class UnbanUserHandler(
    ICurrentUserProvider currentUserProvider,
    IUserSearcher userSearcher,
    IUserRepository userRepository) : IRequestHandler<UnbanUserRequest, Unit>
{
    public async Task<Unit> Handle(UnbanUserRequest request, CancellationToken token)
    {
        var adminId = currentUserProvider.GetUserId();
        var user = await userSearcher.FindByIdAsync(request.UserId, token);

        user.Unban(adminId, request.Reason);
        await userRepository.UpdateUserAsync(user, token);

        return Unit.Value;
    }
}
