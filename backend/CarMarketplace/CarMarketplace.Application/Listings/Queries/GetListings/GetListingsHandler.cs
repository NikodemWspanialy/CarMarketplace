using CarMarketplace.Application.Common.DTOs;
using CarMarketplace.Application.Listings.DTOs;
using CarMarketplace.Application.Listings.Repositories;
using MediatR;

namespace CarMarketplace.Application.Listings.Queries.GetListings;

internal class GetListingsHandler(
    IListingRepository listingRepository)
    : IRequestHandler<GetListingsRequest, ListResponse<ListingResponse>>
{
    public async Task<ListResponse<ListingResponse>> Handle(GetListingsRequest request, CancellationToken token)
    {
        var result = await listingRepository.GetPagedActiveAsync(request.PageNumber, request.PageSize, token);
        var items = result.Listings.Select(ListingResponse.FromEntity).ToList();

        return new ListResponse<ListingResponse>(items, result.TotalCount, request.PageNumber, request.PageSize);
    }
}
