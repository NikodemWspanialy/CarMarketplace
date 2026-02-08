using CarMarketplace.Domain.Users;

namespace CarMarketplace.Application.Authorization.Helpers;

public interface IJwtProvider
{
    string Generate(User user);
}