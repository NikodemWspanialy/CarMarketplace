using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Application.Contacts.DTOs;

namespace CarMarketplace.Application.Listings.Commands.RevealListingContacts;

public record RevealListingContactsRequest(Guid ListingId) : ICommand<List<ContactResponse>>;
