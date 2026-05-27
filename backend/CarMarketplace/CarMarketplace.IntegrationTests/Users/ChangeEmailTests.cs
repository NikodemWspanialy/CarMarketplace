using CarMarketplace.Application.Users.Commands.ChangeEmail;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Users;

public class ChangeEmailTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task ChangeEmail_WithValidEmail_UpdatesEmail()
    {
        // Act
        await SendAsync(new ChangeEmailRequest("newemail@example.com"));

        // Assert
        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        user!.Email.Should().Be("newemail@example.com");
    }
}
