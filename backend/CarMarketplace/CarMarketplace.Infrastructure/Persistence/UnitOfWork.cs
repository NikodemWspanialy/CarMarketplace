using CarMarketplace.Application.Common.Interfaces;

namespace CarMarketplace.Infrastructure.Persistence;

public class UnitOfWork(CarMarketplaceDbContext dbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken token = default) =>
        await dbContext.SaveChangesAsync(token);
}