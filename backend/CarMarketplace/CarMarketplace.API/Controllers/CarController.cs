using CarMarketplace.Application.Cars.Commands.AddCarPhoto;
using CarMarketplace.Application.Cars.Commands.AddCarPhotos;
using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Application.Cars.Commands.DeleteCar;
using CarMarketplace.Application.Cars.Commands.DeleteCarPhoto;
using CarMarketplace.Application.Cars.Commands.SetPrimaryCarPhoto;
using CarMarketplace.Application.Cars.Commands.UpdateCar;
using CarMarketplace.Application.Cars.Commands.UpdateCarPrice;
using CarMarketplace.Application.Cars.Commands.UpdatePhotosOrder;
using CarMarketplace.Application.Cars.Queries.GetCar;
using CarMarketplace.Application.Cars.Queries.GetCars;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMarketplace.API.Controllers;

[ApiController]
[Route("api/car")]
public class CarController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCarRequest command, CancellationToken token = default)
    {
        var id = await mediator.Send(command, token);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [Authorize]
    [HttpPut("update-details/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCarRequest command, CancellationToken token = default)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        var result = await mediator.Send(command, token);

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("delete/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token = default)
    {
        await mediator.Send(new DeleteCarRequest(id), token);

        return NoContent();
    }

    [Authorize]
    [HttpPut("update-price/{id:guid}")]
    public async Task<IActionResult> UpdatePrice(Guid id, [FromBody] UpdateCarPriceRequest command, CancellationToken token = default)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        var result = await mediator.Send(command, token);

        return Ok(result);
    }

    [HttpGet("get-details/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken token = default)
    {
        var car = await mediator.Send(new GetCarRequest(id), token);

        return Ok(car);
    }

    [HttpGet("get-details-list")]
    public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken token = default)
    {
        var result = await mediator.Send(new GetCarsRequest(pageNumber, pageSize), token);

        return Ok(result);
    }

    [Authorize]
    [HttpPost("{carId:guid}/photos")]
    public async Task<IActionResult> AddPhoto(Guid carId, [FromBody] AddCarPhotoRequest body, CancellationToken token = default)
    {
        var result = await mediator.Send(body with { CarId = carId }, token);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    [Authorize]
    [HttpPost("{carId:guid}/photos/batch")]
    public async Task<IActionResult> AddPhotos(Guid carId, [FromBody] AddCarPhotosRequest body, CancellationToken token = default)
    {
        var result = await mediator.Send(body with { CarId = carId }, token);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    [Authorize]
    [HttpDelete("{carId:guid}/photos/{photoId:guid}")]
    public async Task<IActionResult> DeletePhoto(Guid carId, Guid photoId, CancellationToken token = default)
    {
        await mediator.Send(new DeleteCarPhotoRequest(carId, photoId), token);

        return NoContent();
    }

    [Authorize]
    [HttpPut("{carId:guid}/photos/{photoId:guid}/set-primary")]
    public async Task<IActionResult> SetPrimaryPhoto(Guid carId, Guid photoId, CancellationToken token = default)
    {
        var result = await mediator.Send(new SetPrimaryCarPhotoRequest(carId, photoId), token);

        return Ok(result);
    }

    [Authorize]
    [HttpPut("{carId:guid}/photos/update-order")]
    public async Task<IActionResult> UpdatePhotosOrder(Guid carId, [FromBody] UpdatePhotosOrderRequest body, CancellationToken token = default)
    {
        await mediator.Send(body with { CarId = carId }, token);

        return NoContent();
    }
}