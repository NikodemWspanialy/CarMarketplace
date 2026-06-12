using CarMarketplace.Application.Listings.Commands.DetachListingContact;
using CarMarketplace.Domain.Listings.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Listings.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Listings;

public class DetachListingContactTests(CarMarketplaceApiFactory factory) : ListingTestBase(factory)
{
    [Fact]
    public async Task Detach_WithAttachedContact_RemovesContactFromListing()
    {
        // Arrange
        var carId = await CreateCarAsync();
        var contactId1 = await CreateContactAsync();
        var contactId2 = await CreateContactEmailAsync();
        var listingId = await CreateListingAsync(carId, [contactId1, contactId2]);

        // Act
        await SendAsync(new DetachListingContactRequest(listingId, contactId2));

        // Assert
        var listing = await TestData.Listings.FirstOrDefaultAsync(l => l.Id == listingId);
        listing!.ContactIds.Should().NotContain(contactId2);
        listing.ContactIds.Should().HaveCount(1);
    }

    [Fact]
    public async Task Detach_WithNotAttachedContact_ThrowsDomainException()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();

        // Act
        var act = () => SendAsync(new DetachListingContactRequest(listingId, Guid.NewGuid()));

        // Assert
        await act.Should().ThrowAsync<ListingContactNotAttached>();
    }
}
