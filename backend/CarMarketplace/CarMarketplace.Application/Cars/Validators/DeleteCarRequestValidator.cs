using CarMarketplace.Application.Cars.Commands.DeleteCar;
using FluentValidation;

namespace CarMarketplace.Application.Cars.Validators;

public class DeleteCarRequestValidator : AbstractValidator<DeleteCarRequest>
{
    public DeleteCarRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
