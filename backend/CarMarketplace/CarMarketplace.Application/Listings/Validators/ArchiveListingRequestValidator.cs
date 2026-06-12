using CarMarketplace.Application.Listings.Commands.ArchiveListing;
using FluentValidation;

namespace CarMarketplace.Application.Listings.Validators;

public class ArchiveListingRequestValidator : AbstractValidator<ArchiveListingRequest>
{
    public ArchiveListingRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
