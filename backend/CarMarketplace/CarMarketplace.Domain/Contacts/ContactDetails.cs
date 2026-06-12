namespace CarMarketplace.Domain.Contacts;

public record ContactDetails
{
    public string? PhoneNumber { get; private set; }

    public string? CountryCode { get; private set; }

    public string? EmailAddress { get; private set; }

    public string? Username { get; private set; }

    // EF Core
    private ContactDetails() { }

    public ContactDetails(string? phoneNumber, string? countryCode, string? emailAddress, string? username)
    {
        PhoneNumber = phoneNumber;
        CountryCode = countryCode;
        EmailAddress = emailAddress;
        Username = username;
    }
}
