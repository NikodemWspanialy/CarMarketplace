using CarMarketplace.Application.Listings.Commands.ArchiveListing;
using CarMarketplace.Application.Listings.Commands.AttachListingContact;
using CarMarketplace.Application.Listings.Commands.CreateListing;
using CarMarketplace.Application.Listings.Commands.DeactivateListing;
using CarMarketplace.Application.Listings.Commands.DeleteListing;
using CarMarketplace.Application.Listings.Commands.DetachListingContact;
using CarMarketplace.Application.Listings.Commands.MarkListingAsSold;
using CarMarketplace.Application.Listings.Commands.ReactivateListing;
using CarMarketplace.Application.Listings.Commands.RegisterListingView;
using CarMarketplace.Application.Listings.Commands.RevealListingContacts;
using CarMarketplace.Application.Listings.Commands.UpdateListingTitle;
using CarMarketplace.Application.Listings.Queries.GetListing;
using CarMarketplace.Application.Listings.Queries.GetListings;
using CarMarketplace.Application.Listings.Queries.GetListingStats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMarketplace.API.Controllers;

[ApiController]
[Route("api/listing")]
public class ListingController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateListingRequest command, CancellationToken token = default)
    {
        var id = await mediator.Send(command, token);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("get-details/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken token = default)
    {
        var result = await mediator.Send(new GetListingRequest(id), token);
        await mediator.Send(new RegisterListingViewRequest(id), token);

        return Ok(result);
    }

    [HttpGet("get-details-list")]
    public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken token = default)
    {
        var result = await mediator.Send(new GetListingsRequest(pageNumber, pageSize), token);

        return Ok(result);
    }

    [Authorize]
    [HttpPut("update-title/{id:guid}")]
    public async Task<IActionResult> UpdateTitle(Guid id, [FromBody] UpdateListingTitleRequest command, CancellationToken token = default)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        await mediator.Send(command, token);

        return NoContent();
    }

    [Authorize]
    [HttpDelete("delete/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token = default)
    {
        await mediator.Send(new DeleteListingRequest(id), token);

        return NoContent();
    }

    [Authorize]
    [HttpPatch("{id:guid}/mark-as-sold")]
    public async Task<IActionResult> MarkAsSold(Guid id, CancellationToken token = default)
    {
        await mediator.Send(new MarkListingAsSoldRequest(id), token);

        return NoContent();
    }

    [Authorize]
    [HttpPatch("{id:guid}/archive")]
    public async Task<IActionResult> Archive(Guid id, CancellationToken token = default)
    {
        await mediator.Send(new ArchiveListingRequest(id), token);

        return NoContent();
    }

    [Authorize]
    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id, CancellationToken token = default)
    {
        await mediator.Send(new DeactivateListingRequest(id), token);

        return NoContent();
    }

    [Authorize]
    [HttpPatch("{id:guid}/reactivate")]
    public async Task<IActionResult> Reactivate(Guid id, CancellationToken token = default)
    {
        await mediator.Send(new ReactivateListingRequest(id), token);

        return NoContent();
    }

    [Authorize]
    [HttpPost("{id:guid}/contacts/{contactId:guid}")]
    public async Task<IActionResult> AttachContact(Guid id, Guid contactId, CancellationToken token = default)
    {
        await mediator.Send(new AttachListingContactRequest(id, contactId), token);

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:guid}/contacts/{contactId:guid}")]
    public async Task<IActionResult> DetachContact(Guid id, Guid contactId, CancellationToken token = default)
    {
        await mediator.Send(new DetachListingContactRequest(id, contactId), token);

        return NoContent();
    }

    [Authorize]
    [HttpPost("{id:guid}/contacts/reveal")]
    public async Task<IActionResult> RevealContacts(Guid id, CancellationToken token = default)
    {
        var result = await mediator.Send(new RevealListingContactsRequest(id), token);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id:guid}/stats")]
    public async Task<IActionResult> GetStats(Guid id, CancellationToken token = default)
    {
        var result = await mediator.Send(new GetListingStatsRequest(id), token);

        return Ok(result);
    }
}
