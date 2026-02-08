using CarMarketplace.Application.Authorization.DTOs;
using CarMarketplace.Application.Common.Abstractions;

namespace CarMarketplace.Application.Authorization.Queries.LoginUser;

public record LoginUserQuery(string Email, string Password) : ICommand<AuthResponse>;