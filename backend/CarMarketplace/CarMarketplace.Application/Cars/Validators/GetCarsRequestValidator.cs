using CarMarketplace.Application.Cars.Queries.GetCars;
using CarMarketplace.Application.Common.Validators;
using FluentValidation;

namespace CarMarketplace.Application.Cars.Validators;

public class GetCarsRequestValidator : AbstractValidator<GetCarsRequest>
{
    public GetCarsRequestValidator()
    {
        this.ValidPaging(x => x.PageNumber, x => x.PageSize);
    }
}
