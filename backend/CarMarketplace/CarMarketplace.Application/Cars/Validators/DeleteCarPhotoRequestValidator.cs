using CarMarketplace.Application.Cars.Commands.DeleteCarPhoto;
using FluentValidation;

namespace CarMarketplace.Application.Cars.Validators;

public class DeleteCarPhotoRequestValidator : AbstractValidator<DeleteCarPhotoRequest>
{
    public DeleteCarPhotoRequestValidator()
    {
        RuleFor(x => x.CarId).NotEmpty();
        RuleFor(x => x.PhotoId).NotEmpty();
    }
}
