using CarMarketplace.Application.Contacts.Commands.CreateContact;
using CarMarketplace.Application.Contacts.Commands.DeleteContact;
using CarMarketplace.Application.Contacts.Commands.UpdateContact;
using CarMarketplace.Application.Contacts.Queries.GetContacts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMarketplace.API.Controllers;

[ApiController]
[Route("api/user/contacts")]
[Authorize]
public class ContactController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContactRequest command, CancellationToken token = default)
    {
        var id = await mediator.Send(command, token);

        return StatusCode(StatusCodes.Status201Created, new { id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token = default)
    {
        var result = await mediator.Send(new GetContactsRequest(), token);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContactRequest command, CancellationToken token = default)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        var result = await mediator.Send(command, token);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token = default)
    {
        await mediator.Send(new DeleteContactRequest(id), token);

        return NoContent();
    }
}
