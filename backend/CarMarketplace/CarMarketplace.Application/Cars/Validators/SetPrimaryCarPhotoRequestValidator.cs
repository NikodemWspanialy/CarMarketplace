using CarMarketplace.Application.Cars.Commands.SetPrimaryCarPhoto;
using FluentValidation;

namespace CarMarketplace.Application.Cars.Validators;

public class SetPrimaryCarPhotoRequestValidator : AbstractValidator<SetPrimaryCarPhotoRequest>
{
    public SetPrimaryCarPhotoRequestValidator()
    {
        RuleFor(x => x.CarId).NotEmpty();
        RuleFor(x => x.PhotoId).NotEmpty();
    }
}
