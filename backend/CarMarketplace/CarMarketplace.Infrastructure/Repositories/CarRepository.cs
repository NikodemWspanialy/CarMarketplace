using CarMarketplace.Application.Cars.Repositories;
using CarMarketplace.Domain.Cars;
using CarMarketplace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarMarketplace.Infrastructure.Repositories;

public class CarRepository(CarMarketplaceDbContext dbContext) : ICarRepository
{
    public async Task<Car> AddAsync(Car car, CancellationToken token = default)
    {
        await dbContext.Cars.AddAsync(car, token);

        return car;
    }

    public async Task<Car?> GetByIdAsync(Guid id, CancellationToken token = default) =>
        await dbContext.Cars.FirstOrDefaultAsync(x => x.Id == id, token);

    public Task UpdateAsync(Car car, CancellationToken token = default)
    {
        dbContext.Cars.Update(car);

        return Task.CompletedTask;
    }

    public async Task<(IReadOnlyList<Car> Cars, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize,
        CancellationToken token = default)
    {
        var query = dbContext.Cars.AsNoTracking();
        var totalCount = await query.CountAsync(token);

        var cars = await query
            .OrderByDescending(c => c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);

        return (cars, totalCount);
    }
}