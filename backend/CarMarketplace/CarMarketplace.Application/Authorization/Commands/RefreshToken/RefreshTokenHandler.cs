using CarMarketplace.Application.Authorization.DTOs;
using CarMarketplace.Application.Authorization.Helpers;
using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Application.Users.Searchers;
using MediatR;

namespace CarMarketplace.Application.Authorization.Commands.RefreshToken;

internal class RefreshTokenHandler(
    ICurrentUserProvider currentUserProvider,
    IUserSearcher userSearcher,
    IJwtProvider jwtProvider) : IRequestHandler<RefreshTokenRequest, AuthResponse>
{
    public async Task<AuthResponse> Handle(RefreshTokenRequest request, CancellationToken token)
    {
        var userId = currentUserProvider.GetUserId();
        var user = await userSearcher.FindByIdAsync(userId, token);

        var accessToken = jwtProvider.Generate(user);

        return new AuthResponse(accessToken);
    }
}
