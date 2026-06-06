using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.DetachListingContact;

public record DetachListingContactRequest(Guid ListingId, Guid ContactId) : ICommand<Unit>;
