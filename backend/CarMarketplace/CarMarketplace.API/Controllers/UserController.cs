using CarMarketplace.Application.Users.Commands.ChangeEmail;
using CarMarketplace.Application.Users.Commands.ChangePassword;
using CarMarketplace.Application.Users.Commands.DeleteAccount;
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

    [HttpPut("change-email")]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest command, CancellationToken token = default)
    {
        await mediator.Send(command, token);

        return NoContent();
    }

    [HttpDelete("delete-account")]
    public async Task<IActionResult> DeleteAccount(CancellationToken token = default)
    {
        await mediator.Send(new DeleteAccountRequest(), token);

        return NoContent();
    }
}