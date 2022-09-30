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
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc/>
    public IdentityController(IMediator mediator) => _mediator = mediator;

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

        var registrationResult = await _mediator.Send(new UserMediator.UserRegistrationQuery(userInput));

        return Ok(registrationResult);
    }

    /// <summary>
    /// Attempts to login an user.
    /// </summary>
    /// <param name="loginInput"><see cref="AppUserRegistrationInput"/> instance with the user provided values.</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserLoginResult>> RegisterUser([FromBody] UserLoginInput loginInput)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var loginResult = await _mediator.Send(new UserMediator.UserLoginQuery(loginInput));

        return Ok(loginResult);
    }
}