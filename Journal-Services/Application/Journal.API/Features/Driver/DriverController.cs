using Journal.API.Configurations;
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
[ApiController]
[Produces("application/json")]
[Authorize]
[Route("api/driver")]
[ApiExplorerSettings(GroupName = Constants.Swagger.GENERAL_API)]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class DriverController : ControllerBase
{
    private readonly ISender _sender;

    /// <inheritdoc/>
    public DriverController(ISender sender) => _sender = sender;

    /// <summary>
    /// Gets a driver based on its ID.
    /// </summary>
    /// <param name="driverId">The driver unique identifier.</param>
    /// <returns>An instance of <see cref="DriverModel"/></returns>
    [HttpGet("{driverId:guid}")]
    public async Task<ActionResult<DriverModel>> GetDriverById(Guid driverId)
    {
        var driver = await _sender.Send(new DriverMediator.GetDriverByIdQuery(driverId));

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

        var driverPks = await _sender.Send(new DriverMediator.CreateDriverCommand(model));

        return Created(string.Empty, driverPks.Id);
    }
}