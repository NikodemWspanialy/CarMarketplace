using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Admin.Commands.FeatureListing;

public record FeatureListingRequest(Guid ListingId, DateTime Until) : ICommand<Unit>;
