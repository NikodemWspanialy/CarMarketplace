using CarMarketplace.Application.Listings.Commands.MarkListingAsSold;
using FluentValidation;

namespace CarMarketplace.Application.Listings.Validators;

public class MarkListingAsSoldRequestValidator : AbstractValidator<MarkListingAsSoldRequest>
{
    public MarkListingAsSoldRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
