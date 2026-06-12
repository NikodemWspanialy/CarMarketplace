using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.MarkListingAsSold;

public record MarkListingAsSoldRequest(Guid Id) : ICommand<Unit>;
