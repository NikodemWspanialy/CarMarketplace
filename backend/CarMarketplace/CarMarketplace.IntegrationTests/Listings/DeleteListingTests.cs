using CarMarketplace.Application.Listings.Commands.DeleteListing;
using CarMarketplace.Domain.Listings.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Listings.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Listings;

public class DeleteListingTests(CarMarketplaceApiFactory factory) : ListingTestBase(factory)
{
    [Fact]
    public async Task Delete_WithActiveListing_SetsIsDeletedTrue()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();

        // Act
        await SendAsync(new DeleteListingRequest(listingId));

        // Assert
        var listing = await TestData.Listings.IgnoreQueryFilters().FirstOrDefaultAsync(l => l.Id == listingId);
        listing.Should().NotBeNull();
        listing!.IsDeleted.Should().BeTrue();
    }

    [Fact]
    public async Task Delete_WhenAlreadyDeleted_ThrowsDomainException()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        await SendAsync(new DeleteListingRequest(listingId));

        // Act
        var act = () => SendAsync(new DeleteListingRequest(listingId));

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}
