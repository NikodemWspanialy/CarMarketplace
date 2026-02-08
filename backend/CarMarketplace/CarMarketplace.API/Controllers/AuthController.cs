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
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var id = await mediator.Send(request);

        return Ok(id);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserQuery query)
    {
        var result = await mediator.Send(query);

        return Ok(result);
    }
}