using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Listings.Commands.RevealListingContacts;
using CarMarketplace.Domain.Users;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Listings.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Listings;

public class RevealListingContactsTests(CarMarketplaceApiFactory factory) : ListingTestBase(factory)
{
    [Fact]
    public async Task Reveal_WithValidListing_ReturnsContacts()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        var buyerId = await RegisterBuyerAsync();
        SetCurrentUser(buyerId, UserRole.User);

        // Act
        var contacts = await SendAsync(new RevealListingContactsRequest(listingId));

        // Assert
        contacts.Should().HaveCount(1);
    }

    [Fact]
    public async Task Reveal_AsBuyer_CreatesContactRevealRecord()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        var buyerId = await RegisterBuyerAsync();
        SetCurrentUser(buyerId, UserRole.User);

        // Act
        await SendAsync(new RevealListingContactsRequest(listingId));

        // Assert
        var reveals = await TestData.ContactReveals.Where(r => r.ListingId == listingId).ToListAsync();
        reveals.Should().HaveCount(1);
        reveals[0].ViewerId.Should().Be(buyerId);
    }

    [Fact]
    public async Task Reveal_AsSeller_DoesNotCreateRevealRecord()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();

        // Act (seller reveals own listing)
        await SendAsync(new RevealListingContactsRequest(listingId));

        // Assert
        var reveals = await TestData.ContactReveals.Where(r => r.ListingId == listingId).ToListAsync();
        reveals.Should().BeEmpty();
    }

    [Fact]
    public async Task Reveal_SecondTime_DoesNotDuplicateRevealRecords()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        var buyerId = await RegisterBuyerAsync();
        SetCurrentUser(buyerId, UserRole.User);
        await SendAsync(new RevealListingContactsRequest(listingId));

        // Act
        await SendAsync(new RevealListingContactsRequest(listingId));

        // Assert
        var reveals = await TestData.ContactReveals.Where(r => r.ListingId == listingId).ToListAsync();
        reveals.Should().HaveCount(1);
    }

    private async Task<Guid> RegisterBuyerAsync() =>
        await SendAsync(new RegisterUserRequest(
            Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
}
