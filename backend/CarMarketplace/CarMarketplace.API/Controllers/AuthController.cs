using CarMarketplace.Application.Authorization.Commands.ForgotPassword;
using CarMarketplace.Application.Authorization.Commands.RefreshToken;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Authorization.Commands.ResetPassword;
using CarMarketplace.Application.Authorization.Queries.LoginUser;
using CarMarketplace.API.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CarMarketplace.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken token = default)
    {
        var id = await mediator.Send(request, token);

        return Ok(id);
    }

    [EnableRateLimiting(RateLimitPolicy.Auth)]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserQuery query, CancellationToken token = default)
    {
        var result = await mediator.Send(query, token);

        return Ok(result);
    }

    [EnableRateLimiting(RateLimitPolicy.Auth)]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest command, CancellationToken token = default)
    {
        await mediator.Send(command, token);

        return Ok();
    }

    [EnableRateLimiting(RateLimitPolicy.Auth)]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest command, CancellationToken token = default)
    {
        await mediator.Send(command, token);

        return NoContent();
    }

    [Authorize]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> Refresh(CancellationToken token = default)
    {
        var result = await mediator.Send(new RefreshTokenRequest(), token);

        return Ok(result);
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // Placeholder
        // Stateless JWT — actual token removal handled by client
        return NoContent();
    }
}