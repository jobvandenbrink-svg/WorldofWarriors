using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[ApiController]
[Route("warriors/save-game")]
public class SaveGameController : ControllerBase
{
    private readonly string _dataDir = Path.Combine("data", "users");

    public SaveGameController()
    {
        Directory.CreateDirectory(_dataDir);
    }

    [HttpGet]
    public IActionResult Get()
    {
        var userId = Helpers.GetUserId(Request);
        var path = Path.Combine(_dataDir, $"{userId}.json");

        if (!System.IO.File.Exists(path))
        {
            var empty = new SaveGameFromServer
            {
                _UserId = userId,
                _Version = "1",
                _Data = new { },
                _LastSaveTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };

            var json = JsonSerializer.Serialize(empty);
            System.IO.File.WriteAllText(path, json);
            return Ok(empty);
        }

        var text = System.IO.File.ReadAllText(path);
        var save = JsonSerializer.Deserialize<SaveGameFromServer>(text);
        return Ok(save);
    }

    [HttpPost]
    public IActionResult Post([FromBody] SaveGameFromServer save)
    {
        var userId = save._UserId;
        var path = Path.Combine(_dataDir, $"{userId}.json");

        save._LastSaveTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var json = JsonSerializer.Serialize(save);
        System.IO.File.WriteAllText(path, json);

        var response = new VersionData
        {
            _Version = save._Version,
            _SyncFrequency = 60
        };

        return Ok(response);
    }
}
