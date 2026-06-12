using CarMarketplace.Application.Contacts.Commands.CreateContact;
using CarMarketplace.Domain.Contacts;

namespace CarMarketplace.Tests.Shared.Builders.Contact;

public class CreateContactRequestBuilder : Builder<CreateContactRequest>
{
    private ContactType _type;
    private string? _phoneNumber;
    private string? _countryCode;
    private string? _emailAddress;
    private string? _username;
    private string? _label;

    public CreateContactRequestBuilder()
    {
        _type = ContactType.Phone;
        _phoneNumber = Faker.Phone.PhoneNumber("###-###-###");
        _countryCode = "+48";
        _label = Faker.Lorem.Word();
    }

    public CreateContactRequestBuilder AsEmail()
    {
        _type = ContactType.Email;
        _emailAddress = Faker.Internet.Email();
        _phoneNumber = null;
        _countryCode = null;
        return this;
    }

    public override CreateContactRequest Build() =>
        new(_type, _phoneNumber, _countryCode, _emailAddress, _username, _label);
}
