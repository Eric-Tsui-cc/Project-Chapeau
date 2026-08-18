using Microsoft.AspNetCore.Mvc;

namespace Chapeau.Api.Controllers;

[ApiController]

[Route("api/[controller]")]

public class HealthController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Status = "Ok" });
    }
}