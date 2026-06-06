using CarMarketplace.Application.Authorization.Helpers;
using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Application.Users.Searchers;
using CarMarketplace.Domain.Users.Exceptions;
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

        if (passwordHasher.VerifyHashedPassword(user.PasswordHash, request.NewPassword)) // This is not a good idea
            throw new SamePasswordAsPrevious();

        var newHash = passwordHasher.HashPassword(request.NewPassword);
        user.SetPassword(newHash);

        await userRepository.UpdateUserAsync(user, token);

        return Unit.Value;
    }
}
