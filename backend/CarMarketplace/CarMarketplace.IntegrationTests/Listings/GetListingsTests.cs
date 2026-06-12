using CarMarketplace.Application.Listings.Commands.DeactivateListing;
using CarMarketplace.Application.Listings.Queries.GetListings;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Listings.Base;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CarMarketplace.IntegrationTests.Listings;

public class GetListingsTests(CarMarketplaceApiFactory factory) : ListingTestBase(factory)
{
    [Fact]
    public async Task GetListings_WithActiveListings_ReturnsPagedResult()
    {
        // Arrange
        var contactId = await CreateContactAsync();
        var carId1 = await CreateCarAsync();
        await CreateListingAsync(carId1, contactId);
        var carId2 = await CreateCarAsync();
        await CreateListingAsync(carId2, contactId);

        // Act
        var result = await SendAsync(new GetListingsRequest(1, 10));

        // Assert
        result.Items.Should().HaveCount(2);
        result.TotalCount.Should().Be(2);
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
    }

    [Fact]
    public async Task GetListings_ExcludesDeactivatedListings()
    {
        // Arrange
        var contactId = await CreateContactAsync();
        var carId1 = await CreateCarAsync();
        var listingId1 = await CreateListingAsync(carId1, contactId);
        var carId2 = await CreateCarAsync();
        await CreateListingAsync(carId2, contactId);
        await SendAsync(new DeactivateListingRequest(listingId1));

        // Act
        var result = await SendAsync(new GetListingsRequest(1, 10));

        // Assert
        result.Items.Should().HaveCount(1);
        result.TotalCount.Should().Be(1);
    }

    [Fact]
    public async Task GetListings_WithPaging_RespectsPageSize()
    {
        // Arrange
        var contactId = await CreateContactAsync();
        for (var i = 0; i < 3; i++)
        {
            var carId = await CreateCarAsync();
            await CreateListingAsync(carId, contactId);
        }

        // Act
        var result = await SendAsync(new GetListingsRequest(1, 2));

        // Assert
        result.Items.Should().HaveCount(2);
        result.TotalCount.Should().Be(3);
    }

    [Fact]
    public async Task GetListings_WithInvalidPageNumber_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new GetListingsRequest(0, 10));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task GetListings_WithInvalidPageSize_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new GetListingsRequest(1, 0));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
