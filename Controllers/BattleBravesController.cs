using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("battle-braves")]
public class BattleBravesController : ControllerBase
{
    public class LoginData
    {
        public string AUID { get; set; }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginData data)
    {
        if (string.IsNullOrEmpty(data?.AUID))
            return BadRequest(new { error = "Missing AUID" });

        return Ok(new {
            sessionToken = "TOKEN-" + Guid.NewGuid().ToString("N"),
            gameUserID = "USER-" + Guid.NewGuid().ToString("N"),
            deviceTokenHash = "hash123"
        });
    }
}
