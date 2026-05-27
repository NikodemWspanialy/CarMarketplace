using CarMarketplace.Application.Authorization.Repositories;
using CarMarketplace.Domain.Users;
using CarMarketplace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarMarketplace.Infrastructure.Repositories;

public class PasswordResetTokenRepository(CarMarketplaceDbContext dbContext) : IPasswordResetTokenRepository
{
    public async Task AddAsync(PasswordResetToken token, CancellationToken ct = default) =>
        await dbContext.PasswordResetTokens.AddAsync(token, ct);

    public async Task<PasswordResetToken?> GetByTokenAsync(string token, CancellationToken ct = default) =>
        await dbContext.PasswordResetTokens.FirstOrDefaultAsync(x => x.Token == token, ct);

    public Task UpdateAsync(PasswordResetToken token, CancellationToken ct = default)
    {
        dbContext.PasswordResetTokens.Update(token);

        return Task.CompletedTask;
    }
}
