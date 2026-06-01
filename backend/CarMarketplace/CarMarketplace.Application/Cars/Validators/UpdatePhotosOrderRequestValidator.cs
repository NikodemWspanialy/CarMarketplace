using CarMarketplace.Application.Cars.Commands.UpdatePhotosOrder;
using FluentValidation;

namespace CarMarketplace.Application.Cars.Validators;

public class UpdatePhotosOrderRequestValidator : AbstractValidator<UpdatePhotosOrderRequest>
{
    public UpdatePhotosOrderRequestValidator()
    {
        RuleFor(x => x.CarId).NotEmpty();
        RuleFor(x => x.Photos).NotEmpty();
        RuleFor(x => x.Photos)
            .Must(photos => photos.Select(p => p.NewOrder).Distinct().Count() == photos.Count)
            .WithMessage("Photo order values must be unique.");
    }
}
