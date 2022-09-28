using Journal.Domain.Models.Vehicle;
using Journal.Infrastructure.Features.Vehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Features.Vehicle;

/// <summary>
/// Defines the controller to handle the vehicle endpoints.
/// </summary>
[ApiController]
[Route("api/vehicle")]
public class VehicleController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc/>
    public VehicleController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Get all vehicle brands.
    /// </summary>
    /// <returns>A collection of <see cref="VehicleBrand"/></returns>
    [HttpGet("getAllBrands")]
    public async Task<ActionResult<IList<VehicleBrand>>> GetAllBrands()
    {
        var brands = await _mediator.Send(new VehicleMediator.AllVehicleBrandQuery());

        brands = brands
            .OrderBy(b => b.Name)
            .ToList();

        return Ok(brands);
    }
}