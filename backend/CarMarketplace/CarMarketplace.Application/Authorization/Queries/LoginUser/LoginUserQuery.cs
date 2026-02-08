using CarMarketplace.Application.Authorization.DTOs;
using MediatR;

namespace CarMarketplace.Application.Authorization.Queries.LoginUser;

public record LoginUserQuery(string Email, string Password) : IRequest<AuthResponse>;