using CarMarketplace.Domain.Cars;

namespace CarMarketplace.Application.Cars.Repositories;

public interface ICarRepository
{
    Task<Car> AddAsync(Car car, CancellationToken token = default);

    Task<Car?> GetByIdAsync(Guid id, CancellationToken token = default);

    Task UpdateAsync(Car car, CancellationToken token = default);

    Task<(IReadOnlyList<Car> Cars, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken token = default);
}