using System.Security.Cryptography;
using CarMarketplace.Application.Authorization.Repositories;
using CarMarketplace.Domain.Users;

namespace CarMarketplace.Application.Authorization.Helpers;

internal interface IPasswordResetTokenGenerator
{
    Task<string> GenerateAsync(Guid userId, CancellationToken token = default);
}

internal class PasswordResetTokenGenerator(
    IPasswordResetTokenRepository tokenRepository) : IPasswordResetTokenGenerator
{
    public async Task<string> GenerateAsync(Guid userId, CancellationToken token = default)
    {
        var resetToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(48));
        var passwordResetToken = new PasswordResetToken(
            userId,
            resetToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1));

        await tokenRepository.AddAsync(passwordResetToken, token);

        return resetToken;
    }
}
