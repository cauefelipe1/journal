using Journal.API.Extensions;
using Journal.Domain.Models.Driver;
using Journal.Infrastructure.Features.Driver;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Features.Driver;

/// <summary>
/// Defines the controller to handle the driver endpoints.
/// </summary>
[AllowAnonymous]
[ApiController]
[Produces("application/json")]
[Authorize]
[Route("api/driver")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class DriverController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc/>
    public DriverController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Gets a driver based on its ID.
    /// </summary>
    /// <param name="driverId">The driver unique identifier.</param>
    /// <returns>An instance of <see cref="DriverModel"/></returns>
    [HttpGet("{driverId:int}")]
    public async Task<ActionResult<DriverModel>> GetDriverById(int driverId)
    {
        var driver = await _mediator.Send(new DriverMediator.GetDriverByIdQuery(driverId));

        if (driver is null)
            return NotFound();

        return Ok(driver);
    }

    /// <summary>
    /// Creates a new driver in the application.
    /// </summary>
    /// <param name="input">The input for creating a new driver.</param>
    /// <returns>The ID of the driver saved.</returns>
    [HttpPost]
    public async Task<ActionResult<long>> CreateDriver(CreateDriverInput input)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var model = new DriverModel
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            CountryId = input.CountryId,
            UserId = this.GetUserSecondaryId()
        };

        long driverId = await _mediator.Send(new DriverMediator.CreateDriverByIdQuery(model));

        return Created(string.Empty, driverId);
    }
}