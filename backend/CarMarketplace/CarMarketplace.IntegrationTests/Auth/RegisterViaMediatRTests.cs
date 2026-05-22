using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CarMarketplace.IntegrationTests.Auth;

public class RegisterViaMediatRTests(CarMarketplaceApiFactory factory) : IntegrationTestBase(factory)
{
    [Fact]
    public async Task Register_WithValidData_ReturnsUserId()
    {
        // Arrange
        var command = new RegisterUserRequest("test@example.com", "StrongPassword123!", "John", "Doe");

        // Act
        var userId = await SendAsync(command);

        // Assert
        userId.Should().NotBeEmpty();
    }
}
