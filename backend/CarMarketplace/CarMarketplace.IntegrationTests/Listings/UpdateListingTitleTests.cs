using CarMarketplace.Application.Listings.Commands.UpdateListingTitle;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Listings.Base;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Listings;

public class UpdateListingTitleTests(CarMarketplaceApiFactory factory) : ListingTestBase(factory)
{
    [Fact]
    public async Task UpdateTitle_WithValidTitle_UpdatesTitleInDb()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        var newTitle = Faker.Commerce.ProductName();

        // Act
        await SendAsync(new UpdateListingTitleRequest(listingId, newTitle));

        // Assert
        var listing = await TestData.Listings.FirstOrDefaultAsync(l => l.Id == listingId);
        listing!.Title.Should().Be(newTitle);
        listing.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateTitle_WithEmptyTitle_ThrowsValidationException()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();

        // Act
        var act = () => SendAsync(new UpdateListingTitleRequest(listingId, ""));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task UpdateTitle_WithTooLongTitle_ThrowsValidationException()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        var longTitle = new string('x', 201);

        // Act
        var act = () => SendAsync(new UpdateListingTitleRequest(listingId, longTitle));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
