using CarMarketplace.Application.Users.Commands.UpdateUserProfile;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Users;

public class UpdateUserProfileTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task UpdateProfile_WithValidData_ReturnsUpdatedProfile()
    {
        // Act
        var result = await SendAsync(new UpdateUserProfileRequest("Updated", "Name"));

        // Assert
        result.Should().NotBeNull();
        result.FirstName.Should().Be("Updated");
        result.LastName.Should().Be("Name");
    }
}
