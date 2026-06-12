using CarMarketplace.Application.Listings.Commands.RegisterListingView;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Listings.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Listings;

public class RegisterListingViewTests(CarMarketplaceApiFactory factory) : ListingTestBase(factory)
{
    [Fact]
    public async Task RegisterView_WithValidListing_PersistsViewInDb()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();

        // Act
        await SendAsync(new RegisterListingViewRequest(listingId));

        // Assert
        var views = await TestData.ListingViews.Where(v => v.ListingId == listingId).ToListAsync();
        views.Should().HaveCount(1);
        views[0].ViewerId.Should().Be(UserId);
        views[0].IpAddress.Should().Be("127.0.0.1");
    }

    [Fact]
    public async Task RegisterView_SecondTimeWithin24h_DoesNotDuplicateView()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        await SendAsync(new RegisterListingViewRequest(listingId));

        // Act
        await SendAsync(new RegisterListingViewRequest(listingId));

        // Assert
        var views = await TestData.ListingViews.Where(v => v.ListingId == listingId).ToListAsync();
        views.Should().HaveCount(1);
    }
}
