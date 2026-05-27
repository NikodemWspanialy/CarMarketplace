using CarMarketplace.Application.Users.Queries.GetUserProfile;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Users;

public class GetUserProfileTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task GetProfile_WhenUserExists_ReturnsCorrectData()
    {
        // Act
        var result = await SendAsync(new GetUserProfileRequest());

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(UserEmail);
        result.FirstName.Should().Be(UserFirstName);
        result.LastName.Should().Be(UserLastName);
    }
}
