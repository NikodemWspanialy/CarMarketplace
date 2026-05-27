using CarMarketplace.Domain.Users;

namespace CarMarketplace.Application.Authorization.Repositories;

public interface IPasswordResetTokenRepository
{
    Task AddAsync(PasswordResetToken token, CancellationToken ct = default);

    Task<PasswordResetToken?> GetByTokenAsync(string token, CancellationToken ct = default);

    Task UpdateAsync(PasswordResetToken token, CancellationToken ct = default);
}
