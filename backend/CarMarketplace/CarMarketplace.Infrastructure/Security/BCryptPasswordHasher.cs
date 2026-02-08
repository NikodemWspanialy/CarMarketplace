using CarMarketplace.Application.Authorization.Helpers;

namespace CarMarketplace.Infrastructure.Security;

public class BCryptPasswordHasher : IPasswordHasher
{
    public string HashPassword(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyHashedPassword(string hashedPassword, string providedPassword) =>
        BCrypt.Net.BCrypt.Verify(hashedPassword, providedPassword);
}