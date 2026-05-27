using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Users.Helpers;
using MediatR;

namespace CarMarketplace.Application.Users.Commands.DeleteAccount;

internal class DeleteAccountHandler(
    ICurrentUserProvider currentUserProvider,
    IUserService userService) : IRequestHandler<DeleteAccountRequest, Unit>
{
    public async Task<Unit> Handle(DeleteAccountRequest request, CancellationToken token)
    {
        var userId = currentUserProvider.GetUserId();
        await userService.DeleteAsync(userId, token);

        return Unit.Value;
    }
}
