using Journal.API.Base;
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
[Route("app/mobile/driver")]
[ApiExplorerSettings(GroupName = Constants.Swagger.MOBILE_API)]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class DriverMobileController : ControllerBase
{
    private readonly ISender _sender;

    /// <inheritdoc/>
    public DriverMobileController(ISender sender) => _sender = sender;

    /// <summary>
    /// Gets a driver based on its ID.
    /// </summary>
    /// <param name="driverId">The driver unique identifier.</param>
    /// <returns>An instance of <see cref="DriverModel"/></returns>
    [HttpGet("{driverId:guid}")]
    public async Task<ActionResult<ApiResponse<DriverMobileModel>>> GetDriverById(Guid driverId)
    {
        var driver = await _sender.Send(new DriverMediator.GetDriverByIdQuery(driverId));

        if (driver is null)
            return NotFound();

        var result = DriverMobileModel.FromModel(driver);

        return Ok(ApiResponse<DriverMobileModel>.WithSuccess(result));
    }

    /// <summary>
    /// Creates a new driver in the application.
    /// </summary>
    /// <param name="input">The input for creating a new driver.</param>
    /// <returns>The ID of the driver saved.</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateDriver(CreateDriverInput input)
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

        return Created(string.Empty, driverPks.SecondaryId);
    }
}