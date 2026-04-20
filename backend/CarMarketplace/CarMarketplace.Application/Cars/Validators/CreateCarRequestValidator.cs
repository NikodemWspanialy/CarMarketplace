using CarMarketplace.Application.Cars.Commands.CreateCar;
using FluentValidation;

namespace CarMarketplace.Application.Cars.Validators;

public class CreateCarRequestValidator : AbstractValidator<CreateCarRequest>
{
    public CreateCarRequestValidator()
    {
        RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Model).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Year).GreaterThan(1900).LessThanOrEqualTo(DateTime.UtcNow.Year + 1);
        RuleFor(x => x.PriceAmount).GreaterThan(0);
        RuleFor(x => x.PriceCurrency).NotEmpty().MaximumLength(3);
        RuleFor(x => x.Mileage).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FuelType).IsInEnum();
        RuleFor(x => x.Description).MaximumLength(2000);
    }
}
