using CarMarketplace.Application.Cars.Exceptions;
using CarMarketplace.Application.Cars.Repositories;
using CarMarketplace.Domain.Cars;

namespace CarMarketplace.Application.Cars.Searchers;

internal interface ICarSearcher
{
    Task<Car> FindByIdAsync(Guid id, CancellationToken token = default);
}

internal class CarSearcher(ICarRepository carRepository) : ICarSearcher
{
    public async Task<Car> FindByIdAsync(Guid id, CancellationToken token = default) =>
        await carRepository.GetByIdAsync(id, token) ?? throw new CarNotFound(id);
}
