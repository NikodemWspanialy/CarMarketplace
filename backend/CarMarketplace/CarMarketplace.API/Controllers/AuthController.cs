using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Authorization.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserQuery query, CancellationToken token = default)
    {
        var result = await mediator.Send(query, token);

        return Ok(result);
    }
}