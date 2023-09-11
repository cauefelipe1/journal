using Journal.API.Base;
using Journal.API.Configurations;
using Journal.API.Extensions;
using Journal.Identity.Features.Jwt;
using Journal.Identity.Features.User;
using Journal.Identity.Models.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Features.Identity;

/// <summary>
/// Defines the controller to handle the system identity.
/// </summary>
[ApiController]
[Produces("application/json")]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[Route("app/mobile/identity")]
[ApiExplorerSettings(GroupName = Constants.Swagger.MOBILE_API)]
public class IdentityMobileController : ControllerBase
{
    private readonly ISender _sender;

    /// <inheritdoc/>
    public IdentityMobileController(ISender sender) => _sender = sender;

    /// <summary>
    /// Register a new user into the application.
    /// </summary>
    /// <param name="userInput"><see cref="AppUserRegistrationInput"/> instance with the user provided values.</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("registerUser")]
    public async Task<ActionResult<UserRegistrationResult>> RegisterUser([FromBody] AppUserRegistrationInput userInput)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var registrationResult = await _sender.Send(new UserMediator.UserRegistrationQuery(userInput));

        return Ok(registrationResult);
    }

    /// <summary>
    /// Attempts to login an user.
    /// </summary>
    /// <param name="loginInput"><see cref="UserLoginInput"/> instance with the user provided values.</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserLoginResult>> RegisterUser([FromBody] UserLoginInput loginInput)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var loginResult = await _sender.Send(new UserMediator.UserLoginQuery(loginInput));

        return Ok(loginResult);
    }

    /// <summary>
    /// Attempts to refresh an user token.
    /// </summary>
    /// <param name="refreshInput"><see cref="RefreshTokenInput"/> instance with the user provided values.</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("refreshToken")]
    public async Task<ActionResult<UserLoginResult>> RegisterUser([FromBody] RefreshTokenInput refreshInput)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var loginResult = await _sender.Send(new JwtMediator.RefreshTokenQuery(refreshInput));

        return Created(string.Empty, loginResult);
    }

    /// <summary>
    /// Retrieves the information of the logged int user performing the request.
    /// </summary>
    /// <returns></returns>
    [HttpGet("userData")]
    public async Task<ActionResult<ApiResponse<UserDataMobileModel>>> GetUserData()
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var userId = this.GetUserId();

        var userData = await _sender.Send(new UserMediator.GetUserDataQuery(userId));

        if (userData is null)
            return NotFound();

        var result = UserDataMobileModel.FromModel(userData);

        return Ok(ApiResponse<UserDataMobileModel>.WithSuccess(result));
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpGet("checkIfAuthenticated")]
    public ActionResult<bool> CheckIfAuthenticated()
    {
        if (!ModelState.IsValid)
            return BadRequest(false);

        return Ok(true);
    }
}