using CarMarketplace.Application.Users.Commands.UpdateUserProfile;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Users;

public class UpdateUserProfileTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task UpdateProfile_WithValidData_UpdatesInDb()
    {
        // Arrange
        var newFirstName = Faker.Name.FirstName();
        var newLastName = Faker.Name.LastName();

        // Act
        var result = await SendAsync(new UpdateUserProfileRequest(newFirstName, newLastName));

        // Assert
        result.Should().NotBeNull();
        result.FirstName.Should().Be(newFirstName);
        result.LastName.Should().Be(newLastName);

        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        user!.FirstName.Should().Be(newFirstName);
        user.LastName.Should().Be(newLastName);
    }

    [Fact]
    public async Task UpdateProfile_WithEmptyFirstName_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new UpdateUserProfileRequest("", Faker.Name.LastName()));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();

        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        user!.FirstName.Should().Be(UserFirstName);
    }

    [Fact]
    public async Task UpdateProfile_WithEmptyLastName_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new UpdateUserProfileRequest(Faker.Name.FirstName(), ""));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();

        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        user!.LastName.Should().Be(UserLastName);
    }
}
