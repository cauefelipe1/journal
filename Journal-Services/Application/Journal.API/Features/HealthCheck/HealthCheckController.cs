using Journal.Infrastructure.Features.HealthCheck;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Features.HealthCheck;

[ApiController]
public class HealthCheckController : ControllerBase
{
    private readonly IMediator _mediator;

    public HealthCheckController(IMediator mediator) => _mediator = mediator;

    [HttpGet("isDatabaseAlive")]
    public async Task<ActionResult<bool>> GetIsDatabaseAlive()
    {
        bool isDbAlive = await _mediator.Send(new HealthCheckMediator.HealthCheckQuery());

        return isDbAlive;
    }
}