using CarMarketplace.Application.Listings.Commands.DeactivateListing;
using CarMarketplace.Application.Listings.Commands.MarkListingAsSold;
using CarMarketplace.Domain.Listings;
using CarMarketplace.Domain.Listings.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Listings.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Listings;

public class MarkListingAsSoldTests(CarMarketplaceApiFactory factory) : ListingTestBase(factory)
{
    [Fact]
    public async Task MarkAsSold_WithActiveListing_SetsStatusToSold()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();

        // Act
        await SendAsync(new MarkListingAsSoldRequest(listingId));

        // Assert
        var listing = await TestData.Listings.FirstOrDefaultAsync(l => l.Id == listingId);
        listing!.Status.Should().Be(ListingStatus.Sold);
        listing.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task MarkAsSold_WhenAlreadySold_ThrowsDomainException()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        await SendAsync(new MarkListingAsSoldRequest(listingId));

        // Act
        var act = () => SendAsync(new MarkListingAsSoldRequest(listingId));

        // Assert
        await act.Should().ThrowAsync<ListingAlreadySold>();
    }

    [Fact]
    public async Task MarkAsSold_WhenDeactivated_ThrowsDomainException()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        await SendAsync(new DeactivateListingRequest(listingId));

        // Act
        var act = () => SendAsync(new MarkListingAsSoldRequest(listingId));

        // Assert
        await act.Should().ThrowAsync<InvalidListingStatusTransition>();
    }
}
