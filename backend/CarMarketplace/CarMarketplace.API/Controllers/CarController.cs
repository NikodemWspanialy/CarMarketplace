using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Application.Cars.Commands.DeleteCar;
using CarMarketplace.Application.Cars.Commands.UpdateCar;
using CarMarketplace.Application.Cars.Queries.GetCar;
using CarMarketplace.Application.Cars.Queries.GetCars;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarMarketplace.API.Controllers;

[ApiController]
[Route("api/car")]
public class CarController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCarRequest command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("update-details/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCarRequest command)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mediator.Send(new DeleteCarRequest(id));
        return NoContent();
    }

    [HttpGet("get-details/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var car = await mediator.Send(new GetCarRequest(id));
        return Ok(car);
    }

    [HttpGet("get-details-list")]
    public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await mediator.Send(new GetCarsRequest(pageNumber, pageSize));
        return Ok(result);
    }
}