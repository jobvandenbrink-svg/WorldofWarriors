var builder = WebApplication.CreateBuilder(args);

// Read Railway's assigned port
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

builder.WebHost.ConfigureKestrel(options =>
{
    // Listen on the port Railway provides
    options.ListenAnyIP(int.Parse(port));
});

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
