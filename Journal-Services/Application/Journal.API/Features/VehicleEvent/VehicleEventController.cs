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
[Route("api/vehicle_event")]
[ApiExplorerSettings(GroupName = Constants.Swagger.GENERAL_API)]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class VehicleEventController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc/>
    public VehicleEventController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Get all events for a provided secondary id.
    /// </summary>
    /// <param name="vehicleId">The id of the vehicle.</param>
    /// <returns>A collection of <see cref="VehicleEventModel"/></returns>
    [HttpGet("by_vehicle/{vehicleId:guid}")]
    public async Task<ActionResult<IList<VehicleEventModel>>> GetVehicleByMainDriverId(Guid vehicleId)
    {
        var events = await _mediator.Send(new VehicleEventMediator.GetVehicleEventByVehicleQuery(vehicleId));

        return Ok(events);
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

        long eventId = await _mediator.Send(new VehicleEventMediator.CreateVehicleEventQuery(model));

        return Created(string.Empty, eventId);
    }
}