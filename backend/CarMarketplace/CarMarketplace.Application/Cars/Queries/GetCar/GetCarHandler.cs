using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Cars.Searchers;
using MediatR;

namespace CarMarketplace.Application.Cars.Queries.GetCar;

internal class GetCarHandler(
    ICarSearcher carSearcher) 
    : IRequestHandler<GetCarRequest, CarDetailsResponse>
{
    public async Task<CarDetailsResponse> Handle(GetCarRequest request, CancellationToken token)
    {
        var car = await carSearcher.FindByIdAsync(request.Id, token);

        return CarDetailsResponse.FromEntity(car);
    }
}