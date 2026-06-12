using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.ArchiveListing;

public record ArchiveListingRequest(Guid Id) : ICommand<Unit>;
