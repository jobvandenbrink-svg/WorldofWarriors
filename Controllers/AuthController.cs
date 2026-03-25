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
}
