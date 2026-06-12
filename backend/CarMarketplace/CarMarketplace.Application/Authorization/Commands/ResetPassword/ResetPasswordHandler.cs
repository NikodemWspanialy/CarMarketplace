using CarMarketplace.Application.Authorization.Helpers;
using CarMarketplace.Application.Authorization.Repositories;
using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Application.Users.Searchers;
using CarMarketplace.Domain.Users.Exceptions;
using MediatR;

namespace CarMarketplace.Application.Authorization.Commands.ResetPassword;

internal class ResetPasswordHandler(
    IPasswordResetTokenRepository tokenRepository,
    IUserSearcher userSearcher,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher) : IRequestHandler<ResetPasswordRequest, Unit>
{
    public async Task<Unit> Handle(ResetPasswordRequest request, CancellationToken token)
    {
        var resetToken = await tokenRepository.GetByTokenAsync(request.Token, token)
            ?? throw new InvalidResetToken();

        if (!resetToken.IsValid)
            throw new InvalidResetToken();

        var user = await userSearcher.FindByIdAsync(resetToken.UserId, token);

        var newHash = passwordHasher.HashPassword(request.NewPassword);
        user.ChangePassword(newHash);

        resetToken.MarkAsUsed();

        await userRepository.UpdateUserAsync(user, token);
        await tokenRepository.UpdateAsync(resetToken, token);

        return Unit.Value;
    }
}
