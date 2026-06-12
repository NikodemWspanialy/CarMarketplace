using CarMarketplace.Application.Listings.Commands.CreateListing;

namespace CarMarketplace.Tests.Shared.Builders.Listing;

public class CreateListingRequestBuilder : Builder<CreateListingRequest>
{
    private Guid _carId;
    private string _title;
    private List<Guid> _contactIds;

    public CreateListingRequestBuilder()
    {
        _carId = Guid.NewGuid();
        _title = Faker.Commerce.ProductName();
        _contactIds = [];
    }

    public CreateListingRequestBuilder WithCarId(Guid carId) { _carId = carId; return this; }

    public CreateListingRequestBuilder WithTitle(string title) { _title = title; return this; }

    public CreateListingRequestBuilder WithContactIds(List<Guid> contactIds) { _contactIds = contactIds; return this; }

    public CreateListingRequestBuilder WithContactId(Guid contactId) { _contactIds = [contactId]; return this; }

    public override CreateListingRequest Build() =>
        new(_carId, _title, _contactIds);
}
