using CarMarketplace.Application.Authorization.Queries.LoginUser;
using CarMarketplace.Application.Users.Commands.ChangePassword;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Users;

public class ChangePasswordTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task ChangePassword_WithValidOldPassword_AllowsLoginWithNewPassword()
    {
        // Act
        await SendAsync(new ChangePasswordRequest("TestPassword123!", "NewStrongPassword456!"));

        // Assert — can login with new password
        var result = await SendAsync(new LoginUserQuery("user@test.com", "NewStrongPassword456!"));
        result.AccessToken.Should().NotBeNullOrEmpty();
    }
}
