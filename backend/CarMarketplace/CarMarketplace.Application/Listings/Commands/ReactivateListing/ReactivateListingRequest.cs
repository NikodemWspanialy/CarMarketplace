using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.ReactivateListing;

public record ReactivateListingRequest(Guid Id) : ICommand<Unit>;
