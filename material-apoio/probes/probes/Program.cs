using NetDevPack.Utilities;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var ready = true;
app.MapGet("/liveness", (IConfiguration configuration, HttpRequest request) =>
{
    string? parameterTime = request.Headers["X-SLEEP-TIME"];

    if (parameterTime.IsMissing())
        parameterTime = configuration.GetSection("LIVENESS").Value;

    Console.WriteLine(parameterTime);
    Thread.Sleep(TimeSpan.FromSeconds(int.Parse(parameterTime)));

    return ready ? Results.Ok("liveness") : Results.BadRequest();
});

app.MapGet("/startup", (IConfiguration configuration, HttpRequest request) =>
{
    string? parameterTime = request.Headers["X-SLEEP-TIME"];

    if (parameterTime.IsMissing())
        parameterTime = configuration.GetSection("STARTUP").Value;

    Console.WriteLine(parameterTime);
    Thread.Sleep(TimeSpan.FromSeconds(int.Parse(parameterTime)));

    return Results.Ok("startup");
});

app.MapGet("/readiness", (IConfiguration configuration, HttpRequest request) =>
{
    string? parameterTime = request.Headers["X-SLEEP-TIME"];

    if (parameterTime.IsMissing())
        parameterTime = configuration.GetSection("READINESS").Value;

    Console.WriteLine(parameterTime);
    Thread.Sleep(TimeSpan.FromSeconds(int.Parse(parameterTime)));

    return ready ? Results.Ok("readiness") : Results.BadRequest();
});

app.MapGet("/disponivel", () => ready = true);
app.MapGet("/indisponivel", () => ready = false);
app.MapGet("/crash", () =>
{
    throw new Exception("Falha na matrix");
});

int.TryParse(builder.Configuration.GetSection("FIRST-START").Value, out var sleepTime);
Console.WriteLine(sleepTime);
Thread.Sleep(TimeSpan.FromSeconds(sleepTime));

File.Create("/tmp/health");

app.Run();