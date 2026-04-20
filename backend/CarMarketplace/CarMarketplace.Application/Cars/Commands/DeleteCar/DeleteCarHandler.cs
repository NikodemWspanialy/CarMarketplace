using CarMarketplace.Application.Cars.Exceptions;
using CarMarketplace.Application.Cars.Repositories;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.DeleteCar;

internal class DeleteCarHandler(
    ICarRepository carRepository)
    : IRequestHandler<DeleteCarRequest, Unit>
{
    public async Task<Unit> Handle(DeleteCarRequest request, CancellationToken token)
    {
        var car = await carRepository.GetByIdAsync(request.Id, token)
            ?? throw new CarNotFoundException(request.Id);

        car.Delete();

        await carRepository.UpdateAsync(car, token);

        return Unit.Value;
    }
}