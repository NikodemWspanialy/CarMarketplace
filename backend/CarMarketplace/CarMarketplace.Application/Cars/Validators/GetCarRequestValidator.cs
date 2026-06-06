using CarMarketplace.Application.Cars.Queries.GetCar;
using FluentValidation;

namespace CarMarketplace.Application.Cars.Validators;

public class GetCarRequestValidator : AbstractValidator<GetCarRequest>
{
    public GetCarRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
