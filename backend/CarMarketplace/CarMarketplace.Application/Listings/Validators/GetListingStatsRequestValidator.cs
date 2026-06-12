using CarMarketplace.Application.Listings.Queries.GetListingStats;
using FluentValidation;

namespace CarMarketplace.Application.Listings.Validators;

public class GetListingStatsRequestValidator : AbstractValidator<GetListingStatsRequest>
{
    public GetListingStatsRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
