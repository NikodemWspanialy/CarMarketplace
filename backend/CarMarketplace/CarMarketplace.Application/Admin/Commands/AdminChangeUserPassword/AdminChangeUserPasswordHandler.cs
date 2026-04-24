using CarMarketplace.Application.Authorization.Helpers;
using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Application.Users.Searchers;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.AdminChangeUserPassword;

internal class AdminChangeUserPasswordHandler(
    IUserSearcher userSearcher,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher) : IRequestHandler<AdminChangeUserPasswordRequest, Unit>
{
    public async Task<Unit> Handle(AdminChangeUserPasswordRequest request, CancellationToken token)
    {
        var user = await userSearcher.FindByIdAsync(request.UserId, token);

        var newHash = passwordHasher.HashPassword(request.NewPassword);
        user.ChangePassword(newHash, user.PasswordHash);

        await userRepository.UpdateUserAsync(user, token);

        return Unit.Value;
    }
}
