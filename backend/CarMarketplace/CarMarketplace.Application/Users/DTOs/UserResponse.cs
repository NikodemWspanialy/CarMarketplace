using CarMarketplace.Domain.Users;

namespace CarMarketplace.Application.Users.DTOs;

public record UserResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName)
{
    public static UserResponse FromEntity(User user) =>
        new(user.Id,
            user.Email,
            user.FirstName,
            user.LastName);
}
