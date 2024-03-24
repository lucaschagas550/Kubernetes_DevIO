var builder = WebApplication.CreateBuilder(args);
var basePath = builder.Configuration.GetValue<string>("SwaggerPath");

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger(basePath);

var app = builder.Build();
app.UseCustomSwagger(basePath)
    .UsePathBase(new PathString($"/{basePath ?? string.Empty}"));

app.MapPost("/", () =>
{
    var dir = new DirectoryInfo("/data");
    if (!dir.Exists)
        dir.Create();

    var nomeArquivo = dir.EnumerateFiles().Count() + 1;

    File.Create($"/data/{nomeArquivo}");

    return Results.Ok(nomeArquivo);
})
.WithTags("Files");


app.Run();
