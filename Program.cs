var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    // Enable HTTP on port 80
    options.ListenAnyIP(80);

    // Enable HTTPS on port 443 (Railway terminates TLS anyway)
    options.ListenAnyIP(443, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});


builder.Services.AddControllers();

var app = builder.Build();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://0.0.0.0:{port}");

app.MapControllers();

app.Run();
