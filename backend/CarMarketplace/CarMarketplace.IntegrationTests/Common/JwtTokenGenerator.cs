using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CarMarketplace.IntegrationTests.Common;

internal static class JwtTokenGenerator
{
    // Must match appsettings.json values
    private const string Issuer = "CarMarketplace";
    private const string Audience = "CarMarketplaceUsers";
    private const string SecretKey = "SUPER_SECRET_KEY_IMPOSSIBLE_TO_GUESS_123456789";

    public static string Generate(Guid userId, string email, string role = "User")
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
