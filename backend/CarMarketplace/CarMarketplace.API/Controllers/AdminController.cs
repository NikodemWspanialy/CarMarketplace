using CarMarketplace.API.Common;
using CarMarketplace.Application.Admin.Commands.AdminChangeUserPassword;
using CarMarketplace.Application.Admin.Commands.AdminUpdateUserProfile;
using CarMarketplace.Application.Admin.Commands.BanUser;
using CarMarketplace.Application.Admin.Commands.DeleteUser;
using CarMarketplace.Application.Admin.Commands.DowngradeToUser;
using CarMarketplace.Application.Admin.Commands.UnbanUser;
using CarMarketplace.Application.Admin.Commands.UpgradeToAdmin;
using CarMarketplace.Application.Admin.Queries.GetBanHistory;
using CarMarketplace.Application.Admin.Queries.GetUsers;
using CarMarketplace.Application.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMarketplace.API.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Policy = AuthPolicy.AdminOnly)]
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

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken token = default)
    {
        var result = await mediator.Send(new GetUsersRequest(pageNumber, pageSize), token);

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

    [HttpDelete("delete-user/{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken token = default)
    {
        await mediator.Send(new DeleteUserRequest(id), token);

        return NoContent();
    }

    [HttpPut("ban-user/{id:guid}")]
    public async Task<IActionResult> BanUser(Guid id, [FromBody] BanUserRequest body, CancellationToken token = default)
    {
        if (id != body.UserId) return BadRequest("Id mismatch");
        await mediator.Send(body, token);

        return NoContent();
    }

    [HttpPut("unban-user/{id:guid}")]
    public async Task<IActionResult> UnbanUser(Guid id, [FromBody] UnbanUserRequest body, CancellationToken token = default)
    {
        if (id != body.UserId) return BadRequest("Id mismatch");
        await mediator.Send(body, token);

        return NoContent();
    }

    [HttpGet("user/{id:guid}/ban-history")]
    public async Task<IActionResult> GetBanHistory(Guid id, CancellationToken token = default)
    {
        var result = await mediator.Send(new GetBanHistoryRequest(id), token);

        return Ok(result);
    }
}