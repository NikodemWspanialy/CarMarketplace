using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.DeleteListing;

public record DeleteListingRequest(Guid Id) : ICommand<Unit>;
