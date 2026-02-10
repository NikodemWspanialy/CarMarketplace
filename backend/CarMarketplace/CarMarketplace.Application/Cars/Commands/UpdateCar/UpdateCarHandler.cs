using CarMarketplace.Application.Cars.Repositories;
using CarMarketplace.Domain.Cars;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.UpdateCar;

public class UpdateCarHandler(
    ICarRepository carRepository) : IRequestHandler<UpdateCarRequest, Car>
{
    public async Task<Car> Handle(UpdateCarRequest request, CancellationToken token)
    {
        var car = await carRepository.GetByIdAsync(request.Id, token);

        if (car is null)
        {
            // throw domain exception
            throw new NullReferenceException("Car not found");
        }
        car.Update(
            request.Brand,
            request.Model,
            request.Year,
            request.Price,
            request.Mileage,
            request.FuelType,
            request.Description);
        
        await carRepository.UpdateAsync(car, token);

        return car;
    }
}