var builder = WebApplication.CreateBuilder(args);
var basePath = builder.Configuration.GetValue<string>("SwaggerPath");
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger(basePath);

var app = builder.Build();
app.UseCustomSwagger(basePath)
    .UsePathBase(new PathString($"/{basePath ?? string.Empty}"));


app.MapGet("/", () =>
    {
        var dir = new DirectoryInfo("/data");
        if (!dir.Exists)
            return Results.NoContent();

        var files = dir.EnumerateFiles();

        if (files.Any())
            return Results.Ok(new { arquivos = files.Count(), ultimoArquivo = files.Last().Name });

        return Results.NoContent();
    })
.WithTags("Files")
.Produces(200)
.Produces(204)
.WithOpenApi();


app.Run();