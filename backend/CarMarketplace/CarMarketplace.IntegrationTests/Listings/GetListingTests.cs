using CarMarketplace.Application.Listings.Exceptions;
using CarMarketplace.Application.Listings.Queries.GetListing;
using CarMarketplace.Domain.Listings;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Listings.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Listings;

public class GetListingTests(CarMarketplaceApiFactory factory) : ListingTestBase(factory)
{
    [Fact]
    public async Task GetListing_WithExistingId_ReturnsListingDetails()
    {
        // Arrange
        var carId = await CreateCarAsync();
        var contactId = await CreateContactAsync();
        var listingId = await CreateListingAsync(carId, contactId);

        // Act
        var result = await SendAsync(new GetListingRequest(listingId));

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(listingId);
        result.CarId.Should().Be(carId);
        result.Status.Should().Be(ListingStatus.Active);
        result.Contacts.Should().HaveCount(1);
        result.Contacts[0].Id.Should().Be(contactId);
    }

    [Fact]
    public async Task GetListing_WithNonExistingId_ThrowsDomainException()
    {
        // Act
        var act = () => SendAsync(new GetListingRequest(Guid.NewGuid()));

        // Assert
        await act.Should().ThrowAsync<ListingNotFound>();
    }

    [Fact]
    public async Task GetListing_RegistersViewInDb()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();

        // Act
        await SendAsync(new GetListingRequest(listingId));

        // Assert
        var views = await TestData.ListingViews.Where(v => v.ListingId == listingId).ToListAsync();
        views.Should().HaveCount(1);
        views[0].ViewerId.Should().Be(UserId);
        views[0].IpAddress.Should().Be("127.0.0.1");
    }

    [Fact]
    public async Task GetListing_SecondViewWithin24h_DoesNotDuplicateView()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        await SendAsync(new GetListingRequest(listingId));

        // Act
        await SendAsync(new GetListingRequest(listingId));

        // Assert
        var views = await TestData.ListingViews.Where(v => v.ListingId == listingId).ToListAsync();
        views.Should().HaveCount(1);
    }
}
