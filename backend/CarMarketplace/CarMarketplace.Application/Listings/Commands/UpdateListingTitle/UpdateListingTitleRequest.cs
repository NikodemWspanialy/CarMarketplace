using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Listings.Commands.UpdateListingTitle;

public record UpdateListingTitleRequest(Guid Id, string Title) : ICommand<Unit>;
