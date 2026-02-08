using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMarketplace.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    // GET api/test/echo?text=Hello
    [HttpGet("echo")]
    public IActionResult Echo([FromQuery] string text)
    {
        return Ok(new { text });
    }
    
    // GET api/test/echo?text=HelloAuthorization
    [Authorize]
    [HttpGet("echo-authorized")]
    public IActionResult EchoAuthorized([FromQuery] string text)
    {
        return Ok(new { text });
    } 
}