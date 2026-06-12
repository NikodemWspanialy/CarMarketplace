using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarMarketplace.API.tests.Common;

public static class AuthHelper
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static async Task<string> RegisterAndLoginAsync(HttpClient client, string email, string password, string firstName = "Test", string lastName = "User")
    {
        await client.PostAsJsonAsync("/api/auth/register", new
        {
            email,
            password,
            firstName,
            lastName
        });

        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            email,
            password
        });

        var content = await loginResponse.Content.ReadAsStringAsync();
        var authResponse = JsonSerializer.Deserialize<AuthResponseDto>(content, JsonOptions);

        return authResponse!.AccessToken;
    }

    public static void SetBearerToken(HttpClient client, string token)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private record AuthResponseDto(string AccessToken);
}
