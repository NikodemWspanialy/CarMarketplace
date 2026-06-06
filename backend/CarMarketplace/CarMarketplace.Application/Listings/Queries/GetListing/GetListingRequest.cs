using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Application.Listings.DTOs;

namespace CarMarketplace.Application.Listings.Queries.GetListing;

public record GetListingRequest(Guid Id) : IQuery<ListingDetailsResponse>;
