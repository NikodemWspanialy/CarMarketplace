using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.AttachListingContact;

public record AttachListingContactRequest(Guid ListingId, Guid ContactId) : ICommand<Unit>;
