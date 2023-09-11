using Journal.API.Base;
using Journal.API.Configurations;
using Journal.API.Extensions;
using Journal.API.Features.Identity;
using Journal.Domain.Models.Driver;
using Journal.Identity.Features.User;
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
    /// Gets the driver and user data based for the current logged user/driver.
    /// </summary>
    /// <returns>An instance of <see cref="LoggedDriverDataMobileModel"/></returns>
    [HttpGet("logged")]
    public async Task<ActionResult<ApiResponse<LoggedDriverDataMobileModel>>> GetLoggedDriver()
    {
        var userId = this.GetUserId();

        //TODO: Either rewrite to get rid off EF Core or understand how to make it work with mult threads.
        var driver = await _sender.Send(new DriverMediator.GetDriverByUserIdQuery(userId));
        var userData = await _sender.Send(new UserMediator.GetUserDataQuery(userId));

        //await Task.WhenAll(driverTask, userDataTask);

        // var driver = driverTask.Result;
        // var userData = userDataTask.Result;

        if (driver is null || userData is null)
            return NotFound();

        var result = new LoggedDriverDataMobileModel
        {
            Driver = DriverMobileModel.FromModel(driver),
            UserData = UserDataMobileModel.FromModel(userData)
        };

        return Ok(ApiResponse<LoggedDriverDataMobileModel>.WithSuccess(result));
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
            CountryId = input.CountryId
        };

        var driverPks = await _sender.Send(new DriverMediator.CreateDriverCommand(model, this.GetUserId()));

        return Created(string.Empty, driverPks.SecondaryId);
    }
}