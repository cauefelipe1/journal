using Journal.Domain.Models.Vehicle;
using Journal.Infrastructure.Features.Vehicle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Features.Vehicle;

/// <summary>
/// Defines the controller to handle the vehicle endpoints.
/// </summary>
[ApiController]
[Produces("application/json")]
[Authorize]
[Route("api/vehicle")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class VehicleController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc/>
    public VehicleController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Gets a driver based on its ID.
    /// </summary>
    /// <param name="input">The input for creating a new driver.</param>
    /// <returns>The ID of the driver saved.</returns>
    [HttpPost]
    public async Task<ActionResult<int>> CreateVehicle(CreateVehicleInput input)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var model = new VehicleModel
        {
            SecondaryId = input.SecondaryId,
            ModelName = input.ModelName,
            Nickname = input.Nickname,
            ModelYear = input.ModelYear,
            TypeId = input.TypeId,
            BrandId = input.BrandId,
            MainDriverId = input.MainDriverId
        };

        int vehicleId = await _mediator.Send(new VehicleMediator.CreateVehicleQuery(model));

        return Created(vehicleId);
    }

    /// <summary>
    /// Get all vehicle brands.
    /// </summary>
    /// <returns>A collection of <see cref="VehicleBrandModel"/></returns>
    [HttpGet("by_main_driver/{driverId:int}")]
    public async Task<ActionResult<IList<VehicleModel>>> GetVehicleByMainDriverId(int driverId)
    {
        var vehicles = await _mediator.Send(new VehicleMediator.GetVehicleByMainDriverQuery(driverId));

        return Ok(vehicles);
    }

    /// <summary>
    /// Get all vehicle brands.
    /// </summary>
    /// <returns>A collection of <see cref="VehicleBrandModel"/></returns>
    [HttpGet("brands")]
    public async Task<ActionResult<IList<VehicleBrandModel>>> GetAllBrands()
    {
        var brands = await _mediator.Send(new VehicleMediator.AllVehicleBrandQuery());

        brands = brands
            .OrderBy(b => b.Name)
            .ToList();

        return Ok(brands);
    }
}