using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Application.Listings.DTOs;

namespace CarMarketplace.Application.Listings.Queries.GetListingStats;

public record GetListingStatsRequest(Guid Id) : IQuery<ListingStatsResponse>;
