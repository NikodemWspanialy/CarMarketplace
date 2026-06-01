using CarMarketplace.Application.Authorization.Exceptions;
using CarMarketplace.Application.Authorization.Helpers;
using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Application.Users.Searchers;
using CarMarketplace.Domain.Users.Exceptions;
using MediatR;

namespace CarMarketplace.Application.Users.Commands.ChangePassword;

internal class ChangePasswordHandler(
    ICurrentUserProvider currentUserProvider,
    IUserSearcher userSearcher,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher) : IRequestHandler<ChangePasswordRequest, Unit>
{
    public async Task<Unit> Handle(ChangePasswordRequest request, CancellationToken token)
    {
        var userId = currentUserProvider.GetUserId();
        var user = await userSearcher.FindByIdAsync(userId, token);

        if (!passwordHasher.VerifyHashedPassword(user.PasswordHash, request.OldPassword))
            throw new InvalidCredentials();

        if (passwordHasher.VerifyHashedPassword(user.PasswordHash, request.NewPassword))
            throw new SamePasswordAsPrevious();

        var newHash = passwordHasher.HashPassword(request.NewPassword);
        user.SetPassword(newHash);

        await userRepository.UpdateUserAsync(user, token);

        return Unit.Value;
    }
}
