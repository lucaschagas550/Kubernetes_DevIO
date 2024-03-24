using Sodium;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();
app.UseCustomSwagger();

app.MapGet("/cpu", () => Results.Ok(PasswordHash.ScryptHashString(Random.Shared.Next(100_000, 100_000_000).ToString(), PasswordHash.Strength.MediumSlow)));

var _data = new List<byte[]>();

app.MapGet("/memory", () =>
{
    // Cada array tem 1 MB.
    var array = new byte[1024 * 1024];
    Array.Fill(array, (byte)0);

    // Mantém uma referência ao array para que ele não seja liberado pelo garbage collector.
    _data.Add(array);

    return Results.Ok(_data.Count);
});

app.Run();