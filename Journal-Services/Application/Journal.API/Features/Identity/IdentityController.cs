using Journal.Identity.Features.UserRegistration;
using Journal.Identity.Models.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Features.Identity;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator) => _mediator = mediator;

    [HttpPost("registerUser")]
    public async Task<IActionResult> RegisterUser(AppUserRegistrationInput userInput)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var registrationResult = await _mediator.Send(new UserRegistrationMediator.UserRegistrationQuery(userInput));

        return Ok(registrationResult);
    }
}