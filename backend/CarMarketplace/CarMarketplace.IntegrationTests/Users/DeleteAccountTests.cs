using CarMarketplace.Application.Users.Commands.DeleteAccount;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Users;

public class DeleteAccountTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task DeleteAccount_WhenCalled_SoftDeletesUser()
    {
        // Act
        await SendAsync(new DeleteAccountRequest());

        // Assert
        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        user!.IsDeleted.Should().BeTrue();
    }
}
