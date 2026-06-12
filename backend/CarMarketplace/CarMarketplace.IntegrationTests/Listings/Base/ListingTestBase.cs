using CarMarketplace.Application.Contacts.Commands.CreateContact;
using CarMarketplace.Application.Listings.Commands.CreateListing;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using CarMarketplace.Tests.Shared.Builders.Car;
using CarMarketplace.Tests.Shared.Builders.Contact;
using CarMarketplace.Tests.Shared.Builders.Listing;

namespace CarMarketplace.IntegrationTests.Listings.Base;

public abstract class ListingTestBase(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    protected async Task<Guid> CreateActiveListingAsync()
    {
        var carId = await CreateCarAsync();
        var contactId = await CreateContactAsync();

        return await CreateListingAsync(carId, contactId);
    }

    protected async Task<Guid> CreateContactAsync() =>
        await SendAsync(new CreateContactRequestBuilder().Build());

    protected async Task<Guid> CreateContactEmailAsync() =>
        await SendAsync(new CreateContactRequestBuilder().AsEmail().Build());

    protected async Task<Guid> CreateCarAsync() =>
        await SendAsync(new CreateCarRequestBuilder().Build());

    protected async Task<Guid> CreateListingAsync(Guid carId, List<Guid> contactIds) =>
        await SendAsync(new CreateListingRequestBuilder()
            .WithCarId(carId)
            .WithContactIds(contactIds)
            .Build());

    protected async Task<Guid> CreateListingAsync(Guid carId, Guid contactId) =>
        await SendAsync(new CreateListingRequestBuilder()
            .WithCarId(carId)
            .WithContactId(contactId)
            .Build());
}
