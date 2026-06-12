using CarMarketplace.Application.Listings.Exceptions;
using CarMarketplace.Domain.Listings;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Listings.Base;
using CarMarketplace.Tests.Shared.Builders.Car;
using CarMarketplace.Tests.Shared.Builders.Contact;
using CarMarketplace.Tests.Shared.Builders.Listing;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Listings;

public class CreateListingTests(CarMarketplaceApiFactory factory) : ListingTestBase(factory)
{
    [Fact]
    public async Task CreateListing_WithValidData_PersistsListingInDb()
    {
        // Arrange
        var carId = await CreateCarAsync();
        var contactId = await CreateContactAsync();
        var command = new CreateListingRequestBuilder()
            .WithCarId(carId)
            .WithContactId(contactId)
            .Build();

        // Act
        var listingId = await SendAsync(command);

        // Assert
        listingId.Should().NotBeEmpty();
        var listing = await TestData.Listings.FirstOrDefaultAsync(l => l.Id == listingId);
        listing.Should().NotBeNull();
        listing!.CarId.Should().Be(carId);
        listing.SellerId.Should().Be(UserId);
        listing.Title.Should().Be(command.Title);
        listing.Status.Should().Be(ListingStatus.Active);
        listing.IsDeleted.Should().BeFalse();
    }

    [Fact]
    public async Task CreateListing_WithMultipleContacts_AttachesAll()
    {
        // Arrange
        var carId = await CreateCarAsync();
        var contactId1 = await CreateContactAsync();
        var contactId2 = await CreateContactEmailAsync();
        var command = new CreateListingRequestBuilder()
            .WithCarId(carId)
            .WithContactIds([contactId1, contactId2])
            .Build();

        // Act
        var listingId = await SendAsync(command);

        // Assert
        var listing = await TestData.Listings.FirstOrDefaultAsync(l => l.Id == listingId);
        listing!.ContactIds.Should().HaveCount(2);
        listing.ContactIds.Should().Contain(contactId1);
        listing.ContactIds.Should().Contain(contactId2);
    }

    [Fact]
    public async Task CreateListing_WithEmptyTitle_ThrowsValidationException()
    {
        // Arrange
        var carId = await CreateCarAsync();
        var contactId = await CreateContactAsync();
        var command = new CreateListingRequestBuilder()
            .WithCarId(carId)
            .WithContactId(contactId)
            .WithTitle("")
            .Build();

        // Act
        var act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateListing_WithNoContacts_ThrowsValidationException()
    {
        // Arrange
        var carId = await CreateCarAsync();
        var command = new CreateListingRequestBuilder()
            .WithCarId(carId)
            .WithContactIds([])
            .Build();

        // Act
        var act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateListing_WithDuplicateContactIds_ThrowsValidationException()
    {
        // Arrange
        var carId = await CreateCarAsync();
        var contactId = await CreateContactAsync();
        var command = new CreateListingRequestBuilder()
            .WithCarId(carId)
            .WithContactIds([contactId, contactId])
            .Build();

        // Act
        var act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateListing_WhenActiveListingAlreadyExists_ThrowsDomainException()
    {
        // Arrange
        var carId = await CreateCarAsync();
        var contactId = await CreateContactAsync();
        await CreateListingAsync(carId, contactId);

        // Act
        var act = () => CreateListingAsync(carId, contactId);

        // Assert
        await act.Should().ThrowAsync<ActiveListingAlreadyExists>();
    }

    [Fact]
    public async Task CreateListing_WithContactNotOwnedBySeller_ThrowsDomainException()
    {
        // Arrange
        var carId = await CreateCarAsync();
        var command = new CreateListingRequestBuilder()
            .WithCarId(carId)
            .WithContactIds([Guid.NewGuid()])
            .Build();

        // Act
        var act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<ContactsNotOwnedBySeller>();
    }

    [Fact]
    public async Task CreateListing_WithCarNotOwnedBySeller_ThrowsDomainException()
    {
        // Arrange
        var contactId = await CreateContactAsync();
        var command = new CreateListingRequestBuilder()
            .WithCarId(Guid.NewGuid())
            .WithContactId(contactId)
            .Build();

        // Act
        var act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}
