using Journal.Identity.Features.UserRegistration;
using Journal.Identity.Models.Registration;
using Journal.Identity.Models.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Features.Identity;

/// <summary>
/// Defines the controller to handle the system identity.
/// </summary>
[ApiController]
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
    [HttpPost("registerUser")]
    public async Task<ActionResult<UserRegistrationResult>> RegisterUser(AppUserRegistrationInput userInput)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var registrationResult = await _mediator.Send(new UserRegistrationMediator.UserRegistrationQuery(userInput));

        return Ok(registrationResult);
    }
}