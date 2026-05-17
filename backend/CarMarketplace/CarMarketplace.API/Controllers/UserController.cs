using CarMarketplace.Application.Users.Commands.ChangePassword;
using CarMarketplace.Application.Users.Commands.UpdateUserProfile;
using CarMarketplace.Application.Users.Queries.GetUserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMarketplace.API.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile(CancellationToken token = default)
    {
        var result = await mediator.Send(new GetUserProfileRequest(), token);

        return Ok(result);
    }

    [HttpPut("update-profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileRequest command, CancellationToken token = default)
    {
        var result = await mediator.Send(command, token);

        return Ok(result);
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest command, CancellationToken token = default)
    {
        await mediator.Send(command, token);

        return NoContent();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("who-am-i")]
    public IActionResult WhoAmI()
    {
        var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        var email = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

        return Ok(new
        {
            UserId = userId,
            Email = email
        });
    }
}