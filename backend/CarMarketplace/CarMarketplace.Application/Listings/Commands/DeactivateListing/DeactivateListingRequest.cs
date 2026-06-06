using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.DeactivateListing;

public record DeactivateListingRequest(Guid Id) : ICommand<Unit>;
