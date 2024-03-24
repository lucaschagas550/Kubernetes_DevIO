var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();
app.UseCustomSwagger();

app.MapGet("/ping", () => "pong");

if (builder.Configuration.GetValue<bool>("Shutdown"))
    throw new Exception("Aplicacao deu erro desconhecido");

app.Run();
