using CarMarketplace.Application.Admin.Commands.AdminChangeUserPassword;
using CarMarketplace.Application.Admin.Commands.AdminUpdateUserProfile;
using CarMarketplace.Application.Admin.Commands.DowngradeToUser;
using CarMarketplace.Application.Admin.Commands.UpgradeToAdmin;
using CarMarketplace.Application.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMarketplace.API.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Policy = "AdminOnly")]
public class AdminController(IMediator mediator) : ControllerBase
{
    [HttpPut("upgrade-to-admin/{id:guid}")]
    public async Task<IActionResult> UpgradeToAdmin(Guid id, CancellationToken token = default)
    {
        await mediator.Send(new UpgradeToAdminRequest(id), token);

        return NoContent();
    }

    [HttpGet("user/{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken token = default)
    {
        var result = await mediator.Send(new GetUserByIdRequest(id), token);

        return Ok(result);
    }

    [HttpPut("downgrade-to-user/{id:guid}")]
    public async Task<IActionResult> DowngradeToUser(Guid id, CancellationToken token = default)
    {
        await mediator.Send(new DowngradeToUserRequest(id), token);

        return NoContent();
    }

    [HttpPut("update-user-profile/{id:guid}")]
    public async Task<IActionResult> UpdateUserProfile(Guid id, [FromBody] AdminUpdateUserProfileRequest command, CancellationToken token = default)
    {
        if (id != command.UserId) return BadRequest("Id mismatch");
        var result = await mediator.Send(command, token);

        return Ok(result);
    }

    [HttpPut("change-user-password/{id:guid}")]
    public async Task<IActionResult> ChangeUserPassword(Guid id, [FromBody] AdminChangeUserPasswordRequest command, CancellationToken token = default)
    {
        if (id != command.UserId) return BadRequest("Id mismatch");
        await mediator.Send(command, token);

        return NoContent();
    }
}