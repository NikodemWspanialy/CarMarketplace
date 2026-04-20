using CarMarketplace.Application.Cars.Commands.UpdateCar;
using FluentValidation;

namespace CarMarketplace.Application.Cars.Validators;

public class UpdateCarRequestValidator : AbstractValidator<UpdateCarRequest>
{
    public UpdateCarRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Model).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Year).GreaterThan(1900).LessThanOrEqualTo(DateTime.UtcNow.Year + 1);
        RuleFor(x => x.Mileage).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FuelType).IsInEnum();
        RuleFor(x => x.Description).MaximumLength(2000);
    }
}
