using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Auth;

public class RegisterUserTests(CarMarketplaceApiFactory factory) : IntegrationTestBase(factory)
{
    [Fact]
    public async Task Register_WithValidData_CreatesUserAndReturnsId()
    {
        // Arrange
        var command = new RegisterUserRequest("john@example.com", "StrongPassword123!", "John", "Doe");

        // Act
        var userId = await SendAsync(command);

        // Assert
        userId.Should().NotBeEmpty();
        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == userId);
        user.Should().NotBeNull();
        user!.Email.Should().Be("john@example.com");
    }
}
