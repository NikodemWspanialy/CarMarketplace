using CarMarketplace.Application.Admin.Commands.BanUser;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Domain.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class BanUserTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task BanUser_WithValidData_BansUser()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        await SendAsync(new BanUserRequest(userId, Faker.Lorem.Sentence()));

        // Assert
        var user = await TestData.Users
            .Include(u => u.ActiveBan)
            .FirstOrDefaultAsync(u => u.Id == userId);
        user!.IsBanned.Should().BeTrue();
        user.ActiveBan.Should().NotBeNull();
    }

    [Fact]
    public async Task BanUser_WithExpiresAt_BansUserWithExpiration()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        var expiresAt = DateTime.UtcNow.AddDays(7);

        // Act
        await SendAsync(new BanUserRequest(userId, Faker.Lorem.Sentence(), expiresAt));

        // Assert
        var user = await TestData.Users
            .Include(u => u.ActiveBan)
            .FirstOrDefaultAsync(u => u.Id == userId);
        user!.IsBanned.Should().BeTrue();
        user.ActiveBan!.ExpiresAt.Should().BeCloseTo(expiresAt, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task BanUser_WhenAlreadyBanned_ThrowsDomainException()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        await SendAsync(new BanUserRequest(userId, Faker.Lorem.Sentence()));

        // Act
        var act = () => SendAsync(new BanUserRequest(userId, Faker.Lorem.Sentence()));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task BanUser_WithEmptyReason_ThrowsValidationException()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var act = () => SendAsync(new BanUserRequest(userId, ""));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task BanUser_WithPastExpiresAt_ThrowsValidationException()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var act = () => SendAsync(new BanUserRequest(userId, Faker.Lorem.Sentence(), DateTime.UtcNow.AddDays(-1)));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
