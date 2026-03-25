using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("warriors")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register()
    {
        var auid = "AUID-" + Guid.NewGuid().ToString("N");
        var gameUserId = "USER-" + Guid.NewGuid().ToString("N");
        var sessionToken = "TOKEN-" + Guid.NewGuid().ToString("N");

        return Ok(new {
            sessionToken = sessionToken,
            gameUserID = gameUserId,
            AUID = auid
        });
    }

    [HttpPost("/battle-braves/login")]
    public IActionResult Login([FromBody] dynamic body)
    {
        string auid = body?.AUID;

        if (string.IsNullOrEmpty(auid))
            return BadRequest(new { error = "Missing AUID" });

        return Ok(new {
            sessionToken = "TOKEN-" + Guid.NewGuid().ToString("N"),
            gameUserID = "USER-" + Guid.NewGuid().ToString("N"),
            deviceTokenHash = "hash123"
        });
    }
}
