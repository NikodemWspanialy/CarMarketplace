using CarMarketplace.Application.Cars.Commands.AddCarPhotos;
using FluentValidation;

namespace CarMarketplace.Application.Cars.Validators;

public class AddCarPhotosRequestValidator : AbstractValidator<AddCarPhotosRequest>
{
    public AddCarPhotosRequestValidator()
    {
        RuleFor(x => x.CarId).NotEmpty();
        RuleFor(x => x.Photos).NotEmpty().WithMessage("At least one photo is required.");
        RuleForEach(x => x.Photos).ChildRules(photo =>
        {
            photo.RuleFor(p => p.Url)
                .NotEmpty()
                .MaximumLength(2048)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Url must be a valid absolute URI.");
            photo.RuleFor(p => p.Order).GreaterThanOrEqualTo(0);
        });
    }
}
