using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Application.Common.DTOs;
using CarMarketplace.Application.Listings.DTOs;

namespace CarMarketplace.Application.Listings.Queries.GetListings;

public record GetListingsRequest(int PageNumber, int PageSize) : IQuery<ListResponse<ListingResponse>>;
