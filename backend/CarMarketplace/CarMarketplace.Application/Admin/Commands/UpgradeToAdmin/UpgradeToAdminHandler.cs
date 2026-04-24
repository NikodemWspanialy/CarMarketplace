using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Application.Users.Searchers;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.UpgradeToAdmin;

internal class UpgradeToAdminHandler(
    IUserSearcher userSearcher,
    IUserRepository userRepository) : IRequestHandler<UpgradeToAdminRequest, Unit>
{
    public async Task<Unit> Handle(UpgradeToAdminRequest request, CancellationToken token)
    {
        var user = await userSearcher.FindByIdAsync(request.UserId, token);

        user.PromoteToAdmin();

        await userRepository.UpdateUserAsync(user, token);

        return Unit.Value;
    }
}
