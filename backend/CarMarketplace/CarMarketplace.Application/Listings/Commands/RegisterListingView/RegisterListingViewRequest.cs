using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.RegisterListingView;

public record RegisterListingViewRequest(Guid ListingId) : ICommand<Unit>;
