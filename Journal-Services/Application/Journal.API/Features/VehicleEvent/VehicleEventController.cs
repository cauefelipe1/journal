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
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class VehicleEventController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc/>
    public VehicleEventController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Creates a new Vehicle Event.
    /// </summary>
    /// <param name="input">The input for creating the new vehicle event.</param>
    /// <returns>The ID of the driver saved.</returns>
    [HttpPost]
    public async Task<ActionResult<int>> CreateVehicleEvent(CreateVehicleEventInput input)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var model = new VehicleEventModel
        {
            OwnerDriverId = input.OwnerDriverId,
            VehicleId = input.VehicleId,
            Date = input.Date,
            VehicleOdometer = input.VehicleOdometer,
            Type = input.Type,
            DriverId = input.DriverId,
            Description = input.Description,
            Note = input.Note
        };

        int eventId = await _mediator.Send(new VehicleEventMediator.CreateVehicleEventQuery(model));

        return Created(string.Empty, eventId);
    }
}