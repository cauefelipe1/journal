using Journal.API.Configurations;
using Journal.Infrastructure.Features.HealthCheck;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Features.HealthCheck;

/// <summary>
/// Defines the controller to handle the health check endpoints.
/// </summary>
[ApiController]
[Produces("application/json")]
[Authorize]
[Route("api/health_heck/")]
[ApiExplorerSettings(GroupName = Constants.Swagger.GENERAL_API)]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class HealthCheckController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc/>
    public HealthCheckController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Checks if the database is alive or not.
    /// </summary>
    /// <returns>True if alive, false otherwise</returns>
    [HttpGet("isDatabaseAlive")]
    public async Task<ActionResult<bool>> GetIsDatabaseAlive()
    {
        bool isDbAlive = await _mediator.Send(new HealthCheckMediator.HealthCheckQuery());

        return isDbAlive;
    }
}