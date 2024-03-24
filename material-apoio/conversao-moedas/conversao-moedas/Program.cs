using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("cotacoes.json", optional: false, reloadOnChange: true);
builder.Services.Configure<Dictionary<string, decimal>>(builder.Configuration.GetSection("Moedas"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();
app.UseCustomSwagger();


app.MapGet("conversoes", (
    [FromServices] IConfiguration configuration,
    [FromServices] IOptions<Dictionary<string, decimal>> cotacoes) => Results.Ok(cotacoes.Value));

app.MapGet("dolar/{moeda}/{valor}", (
    [FromServices] IConfiguration configuration,
    [FromServices] IOptions<Dictionary<string, decimal>> cotacoes,
    string moeda,
    decimal valor) =>
{
    if (!cotacoes.Value!.ContainsKey(moeda.ToUpper()))
        return Results.NotFound();

    var taxaDeConversao = cotacoes.Value[moeda.ToUpper()];
    var valorConvertido = Math.Round(valor / taxaDeConversao, 2);

    return Results.Ok(valorConvertido);
});

app.MapGet("taxa-cambio", (
    [FromServices] IConfiguration configuration,
    [FromServices] IOptions<Dictionary<string, decimal>> cotacoes,
    string moedaOrigem,
    string moedaDestino,
    decimal valor) =>
{
    if (!cotacoes.Value.ContainsKey(moedaOrigem.ToUpper()))
        return Results.NotFound("Moeda de origem não encontrada.");

    if (!cotacoes.Value.ContainsKey(moedaDestino.ToUpper()))
        return Results.NotFound("Moeda de destino não encontrada.");

    var taxaDeConversaoOrigem = cotacoes.Value[moedaOrigem.ToUpper()];
    var valorEmDolar = valor / taxaDeConversaoOrigem;

    var taxaDeConversaoDestino = cotacoes.Value[moedaDestino.ToUpper()];
    var valorConvertido = valorEmDolar * taxaDeConversaoDestino;

    return Results.Ok(valorConvertido);
});

app.Run();
