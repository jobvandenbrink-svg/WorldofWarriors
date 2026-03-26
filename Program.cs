var builder = WebApplication.CreateBuilder(args);

// Kestrel: HTTP only (Railway handles HTTPS externally)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

builder.Services.AddControllers();

var app = builder.Build();

// Railway sets PORT env var, so bind to it
var port = Environment.GetEnvironmentVariable("PORT") ?? "80";
app.Urls.Add($"http://0.0.0.0:{port}");

app.MapControllers();

app.Run();
