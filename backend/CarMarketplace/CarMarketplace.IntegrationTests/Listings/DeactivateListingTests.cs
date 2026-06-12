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

public class DeactivateListingTests(CarMarketplaceApiFactory factory) : ListingTestBase(factory)
{
    [Fact]
    public async Task Deactivate_FromActive_SetsStatusToDeactivated()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();

        // Act
        await SendAsync(new DeactivateListingRequest(listingId));

        // Assert
        var listing = await TestData.Listings.FirstOrDefaultAsync(l => l.Id == listingId);
        listing!.Status.Should().Be(ListingStatus.Deactivated);
    }

    [Fact]
    public async Task Deactivate_WhenSold_ThrowsDomainException()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        await SendAsync(new MarkListingAsSoldRequest(listingId));

        // Act
        var act = () => SendAsync(new DeactivateListingRequest(listingId));

        // Assert
        await act.Should().ThrowAsync<InvalidListingStatusTransition>();
    }

    [Fact]
    public async Task Deactivate_WhenAlreadyDeactivated_ThrowsDomainException()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        await SendAsync(new DeactivateListingRequest(listingId));

        // Act
        var act = () => SendAsync(new DeactivateListingRequest(listingId));

        // Assert
        await act.Should().ThrowAsync<InvalidListingStatusTransition>();
    }
}
