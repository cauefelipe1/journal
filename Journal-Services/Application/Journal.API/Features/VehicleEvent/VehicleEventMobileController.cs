using Journal.API.Configurations;
using Journal.Domain.Models.VehicleEvent;
using Journal.Infrastructure.Features.VehicleEvent;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Features.VehicleEvent;

/// <summary>
/// Defines the controller to handle the vehicle endpoints.
/// </summary>
[ApiController]
[Produces("application/json")]
[Authorize]
[Route("app/mobile/vehicle_event")]
[ApiExplorerSettings(GroupName = Constants.Swagger.MOBILE_API)]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class VehicleEventMobileController : ControllerBase
{
    private readonly ISender _sender;

    /// <inheritdoc/>
    public VehicleEventMobileController(ISender sender) => _sender = sender;

    /// <summary>
    /// Get all events for a provided secondary id.
    /// </summary>
    /// <param name="vehicleId">The id of the vehicle.</param>
    /// <returns>A collection of <see cref="VehicleEventMobileModel"/></returns>
    [HttpGet("by_vehicle/{vehicleId:guid}")]
    public async Task<ActionResult<IList<VehicleEventMobileModel>>> GetVehicleByMainDriverId(Guid vehicleId)
    {
        var events = await _sender.Send(new VehicleEventMediator.GetVehicleEventByVehicleQuery(vehicleId));

        var result = events.Select(VehicleEventMobileModel.FromModel);

        return Ok(result);
    }

    /// <summary>
    /// Creates a new Vehicle Event.
    /// </summary>
    /// <param name="input">The input for creating the new vehicle event.</param>
    /// <returns>The ID of the driver saved.</returns>
    [HttpPost]
    public async Task<ActionResult<long>> CreateVehicleEvent(CreateVehicleEventInput input)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var model = new VehicleEventModel
        {
            OwnerDriverSecondaryId = input.OwnerDriverId,
            VehicleSecondaryId = input.VehicleId,
            Date = input.Date,
            VehicleOdometer = input.VehicleOdometer,
            Type = input.Type,
            DriverSecondaryId = input.DriverId,
            Description = input.Description,
            Note = input.Note
        };

        var eventPks = await _sender.Send(new VehicleEventMediator.CreateVehicleEventQuery(model));

        return Created(string.Empty, eventPks.SecondaryId);
    }
}