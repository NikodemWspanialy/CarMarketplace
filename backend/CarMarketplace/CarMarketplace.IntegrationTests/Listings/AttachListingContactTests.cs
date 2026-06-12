using CarMarketplace.Application.Listings.Commands.AttachListingContact;
using CarMarketplace.Application.Listings.Exceptions;
using CarMarketplace.Domain.Listings.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Listings.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Listings;

public class AttachListingContactTests(CarMarketplaceApiFactory factory) : ListingTestBase(factory)
{
    [Fact]
    public async Task Attach_WithOwnedContact_AddsContactToListing()
    {
        // Arrange
        var carId = await CreateCarAsync();
        var contactId1 = await CreateContactAsync();
        var contactId2 = await CreateContactEmailAsync();
        var listingId = await CreateListingAsync(carId, contactId1);

        // Act
        await SendAsync(new AttachListingContactRequest(listingId, contactId2));

        // Assert
        var listing = await TestData.Listings.FirstOrDefaultAsync(l => l.Id == listingId);
        listing!.ContactIds.Should().Contain(contactId2);
        listing.ContactIds.Should().HaveCount(2);
    }

    [Fact]
    public async Task Attach_WithAlreadyAttachedContact_ThrowsDomainException()
    {
        // Arrange
        var carId = await CreateCarAsync();
        var contactId = await CreateContactAsync();
        var listingId = await CreateListingAsync(carId, contactId);

        // Act
        var act = () => SendAsync(new AttachListingContactRequest(listingId, contactId));

        // Assert
        await act.Should().ThrowAsync<ListingContactAlreadyAttached>();
    }

    [Fact]
    public async Task Attach_WithContactNotOwnedBySeller_ThrowsDomainException()
    {
        // Arrange
        var carId = await CreateCarAsync();
        var contactId = await CreateContactAsync();
        var listingId = await CreateListingAsync(carId, contactId);

        // Act
        var act = () => SendAsync(new AttachListingContactRequest(listingId, Guid.NewGuid()));

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}
