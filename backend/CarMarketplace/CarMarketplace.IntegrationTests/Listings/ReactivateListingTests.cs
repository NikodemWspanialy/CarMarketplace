using CarMarketplace.Application.Listings.Commands.DeactivateListing;
using CarMarketplace.Application.Listings.Commands.MarkListingAsSold;
using CarMarketplace.Application.Listings.Commands.ReactivateListing;
using CarMarketplace.Domain.Listings;
using CarMarketplace.Domain.Listings.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Listings.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Listings;

public class ReactivateListingTests(CarMarketplaceApiFactory factory) : ListingTestBase(factory)
{
    [Fact]
    public async Task Reactivate_FromDeactivated_SetsStatusToActive()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        await SendAsync(new DeactivateListingRequest(listingId));

        // Act
        await SendAsync(new ReactivateListingRequest(listingId));

        // Assert
        var listing = await TestData.Listings.FirstOrDefaultAsync(l => l.Id == listingId);
        listing!.Status.Should().Be(ListingStatus.Active);
    }

    [Fact]
    public async Task Reactivate_FromActive_ThrowsDomainException()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();

        // Act
        var act = () => SendAsync(new ReactivateListingRequest(listingId));

        // Assert
        await act.Should().ThrowAsync<InvalidListingStatusTransition>();
    }

    [Fact]
    public async Task Reactivate_FromSold_ThrowsDomainException()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        await SendAsync(new MarkListingAsSoldRequest(listingId));

        // Act
        var act = () => SendAsync(new ReactivateListingRequest(listingId));

        // Assert
        await act.Should().ThrowAsync<InvalidListingStatusTransition>();
    }
}
