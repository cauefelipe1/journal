using Journal.API.Configurations;
using Journal.Domain.Models.Vehicle;
using Journal.Infrastructure.Features.Vehicle;
using Journal.Infrastructure.Features.VehicleBrand;
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
[Route("app/mobile/vehicle")]
[ApiExplorerSettings(GroupName = Constants.Swagger.MOBILE_API)]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class VehicleMobileController : ControllerBase
{
    private readonly ISender _sender;

    /// <inheritdoc/>
    public VehicleMobileController(ISender sender) => _sender = sender;

    /// <summary>
    /// Creates a new vehicle.
    /// </summary>
    /// <param name="input">The input for creating a new driver.</param>
    /// <returns>The ID of the driver saved.</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateVehicle(CreateVehicleInput input)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var model = new VehicleModel
        {
            ModelName = input.ModelName,
            Nickname = input.Nickname,
            ModelYear = input.ModelYear,
            Type = input.Type,
            BrandSecondaryId = input.BrandId,
            MainDriverSecondaryId = input.MainDriverId
        };

        var vehiclePks = await _sender.Send(new VehicleMediator.CreateVehicleQuery(model, input.MainDriverId));

        return Created(string.Empty, vehiclePks.SecondaryId);
    }

    /// <summary>
    /// Get all vehicles for a provided driver.
    /// </summary>
    /// <param name="driverId">The id of the driver.</param>
    /// <returns>A collection of <see cref="VehicleMobileModel"/></returns>
    [HttpGet("by_main_driver/{driverId:guid}")]
    public async Task<ActionResult<IList<VehicleMobileModel>>> GetVehicleByMainDriverId(Guid driverId)
    {
        var vehicles = await _sender.Send(new VehicleMediator.GetVehicleByMainDriverQuery(driverId));

        if (vehicles.Count <= 0)
            return NotFound();

        var result = vehicles.Select(VehicleMobileModel.FromModel);

        return Ok(result);
    }

    /// <summary>
    /// Get all vehicle brands.
    /// </summary>
    /// <returns>A collection of <see cref="VehicleBrandModel"/></returns>
    [HttpGet("brands")]
    public async Task<ActionResult<IList<VehicleBrandModel>>> GetAllBrands()
    {
        var brands = await _sender.Send(new VehicleBrandMediator.AllVehicleBrandQuery());

        brands = brands
            .OrderBy(b => b.Name)
            .ToList();

        return Ok(brands);
    }
}