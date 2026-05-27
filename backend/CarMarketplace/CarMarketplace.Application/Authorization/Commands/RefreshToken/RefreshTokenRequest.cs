using CarMarketplace.Application.Authorization.DTOs;
using CarMarketplace.Application.Common.Abstractions;

namespace CarMarketplace.Application.Authorization.Commands.RefreshToken;

public record RefreshTokenRequest : ICommand<AuthResponse>;
