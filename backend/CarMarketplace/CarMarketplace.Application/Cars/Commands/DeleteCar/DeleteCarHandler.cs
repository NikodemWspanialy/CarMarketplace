using CarMarketplace.Application.Cars.Repositories;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.DeleteCar;

public class DeleteCarHandler(
    ICarRepository carRepository)
    : IRequestHandler<DeleteCarRequest, bool>
{
    public async Task<bool> Handle(DeleteCarRequest request, CancellationToken token)
    {
        var car = await carRepository.GetByIdAsync(request.Id, token);

        if (car is null)
        {
            //throw domain exception
            return false;
        }

        car.Delete();

        await carRepository.UpdateAsync(car, token);

        return true;
    }
}