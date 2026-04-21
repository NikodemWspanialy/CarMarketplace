using CarMarketplace.Application.Cars.Commands.UpdateCarPrice;
using FluentValidation;

namespace CarMarketplace.Application.Cars.Validators;

public class UpdateCarPriceRequestValidator : AbstractValidator<UpdateCarPriceRequest>
{
    public UpdateCarPriceRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.PriceAmount).GreaterThan(0);
        RuleFor(x => x.PriceCurrency).MaximumLength(3);
    }
}