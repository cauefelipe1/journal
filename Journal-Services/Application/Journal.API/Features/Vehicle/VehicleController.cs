using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Features.Vehicle;

public class VehicleController : ControllerBase
{
    [HttpGet("get")]
    public IActionResult GetBrands()
    {
        return Ok("I'm working!!");
    }
}