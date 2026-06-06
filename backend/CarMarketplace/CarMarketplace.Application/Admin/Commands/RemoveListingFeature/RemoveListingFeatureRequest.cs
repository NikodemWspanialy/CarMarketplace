using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.RemoveListingFeature;

public record RemoveListingFeatureRequest(Guid ListingId) : ICommand<Unit>;
