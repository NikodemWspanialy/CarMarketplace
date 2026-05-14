using CarMarketplace.Application.Cars.Commands.AddCarPhoto;
using FluentValidation;

namespace CarMarketplace.Application.Cars.Validators;

public class AddCarPhotoRequestValidator : AbstractValidator<AddCarPhotoRequest>
{
    public AddCarPhotoRequestValidator()
    {
        RuleFor(x => x.CarId).NotEmpty();
        RuleFor(x => x.Url)
            .NotEmpty()
            .MaximumLength(2048)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("Url must be a valid absolute URI.");
        RuleFor(x => x.Order).GreaterThanOrEqualTo(0);
    }
}