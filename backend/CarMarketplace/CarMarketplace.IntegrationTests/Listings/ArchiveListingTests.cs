using CarMarketplace.Application.Listings.Commands.ArchiveListing;
using CarMarketplace.Application.Listings.Commands.DeactivateListing;
using CarMarketplace.Application.Listings.Commands.MarkListingAsSold;
using CarMarketplace.Domain.Listings;
using CarMarketplace.Domain.Listings.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Listings.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Listings;

public class ArchiveListingTests(CarMarketplaceApiFactory factory) : ListingTestBase(factory)
{
    [Fact]
    public async Task Archive_FromActive_SetsStatusToArchived()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();

        // Act
        await SendAsync(new ArchiveListingRequest(listingId));

        // Assert
        var listing = await TestData.Listings.FirstOrDefaultAsync(l => l.Id == listingId);
        listing!.Status.Should().Be(ListingStatus.Archived);
    }

    [Fact]
    public async Task Archive_FromSold_SetsStatusToArchived()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        await SendAsync(new MarkListingAsSoldRequest(listingId));

        // Act
        await SendAsync(new ArchiveListingRequest(listingId));

        // Assert
        var listing = await TestData.Listings.FirstOrDefaultAsync(l => l.Id == listingId);
        listing!.Status.Should().Be(ListingStatus.Archived);
    }

    [Fact]
    public async Task Archive_FromDeactivated_SetsStatusToArchived()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        await SendAsync(new DeactivateListingRequest(listingId));

        // Act
        await SendAsync(new ArchiveListingRequest(listingId));

        // Assert
        var listing = await TestData.Listings.FirstOrDefaultAsync(l => l.Id == listingId);
        listing!.Status.Should().Be(ListingStatus.Archived);
    }

    [Fact]
    public async Task Archive_WhenAlreadyArchived_ThrowsDomainException()
    {
        // Arrange
        var listingId = await CreateActiveListingAsync();
        await SendAsync(new ArchiveListingRequest(listingId));

        // Act
        var act = () => SendAsync(new ArchiveListingRequest(listingId));

        // Assert
        await act.Should().ThrowAsync<InvalidListingStatusTransition>();
    }
}
