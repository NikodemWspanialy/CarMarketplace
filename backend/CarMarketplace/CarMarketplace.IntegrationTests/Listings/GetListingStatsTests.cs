using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Listings.Commands.RegisterListingView;
using CarMarketplace.Application.Listings.Commands.RevealListingContacts;
using CarMarketplace.Application.Listings.Queries.GetListingStats;
using CarMarketplace.Domain.Users;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Listings.Base;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Listings;

public class GetListingStatsTests(CarMarketplaceApiFactory factory) : ListingTestBase(factory)
{
    [Fact]
    public async Task GetStats_WithNoViews_ReturnsZeros()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();

        // Act
        var stats = await SendAsync(new GetListingStatsRequest(listingId));

        // Assert
        stats.ViewCount.Should().Be(0);
        stats.ContactRevealsCount.Should().Be(0);
    }

    [Fact]
    public async Task GetStats_AfterViewsAndReveals_ReturnsCorrectCounts()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();

        // Register a view and reveal from a buyer
        var buyerId = await SendAsync(new RegisterUserRequest(
            Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        SetCurrentUser(buyerId, UserRole.User);
        await SendAsync(new RegisterListingViewRequest(listingId));
        await SendAsync(new RevealListingContactsRequest(listingId));

        // Switch back to seller
        SetCurrentUser(UserId, UserRole.User);

        // Act
        var stats = await SendAsync(new GetListingStatsRequest(listingId));

        // Assert
        stats.ViewCount.Should().Be(1);
        stats.ContactRevealsCount.Should().Be(1);
    }

    [Fact]
    public async Task GetStats_AsNonOwner_ThrowsUnauthorized()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();

        var otherUserId = await SendAsync(new RegisterUserRequest(
            Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        SetCurrentUser(otherUserId, UserRole.User);

        // Act
        var act = () => SendAsync(new GetListingStatsRequest(listingId));

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
