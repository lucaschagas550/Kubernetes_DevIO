using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.Configuration;
public class BasePathDocumentFilter : IDocumentFilter
{
    private readonly string _basePath;

    public BasePathDocumentFilter(string basePath)
    {
        _basePath = basePath;
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{_basePath}" } };
    }
}

public static class CustomConfigurationSwagger
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services, string? basePath)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Create - Volume Demo",
                Contact = new OpenApiContact()
                {
                    Email = "bhdebrito@gmail.com",
                    Name = "Bruno Brito",
                    Url = new Uri("https://brunobrito.net.br")
                }
            });
            c.DocumentFilter<BasePathDocumentFilter>(basePath != null ? $"/{basePath}" : "/");
        });

        return services;
    }

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, string? basePath)
    {
        app
            .UseSwagger(c => c.RouteTemplate = basePath != null ? $"{basePath}/swagger/{{documentName}}/swagger.json" : c.RouteTemplate)
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"v1/swagger.json", "Create data - Volume Demo V1");
                c.RoutePrefix = basePath != null ? $"{basePath}/swagger" : c.RoutePrefix;
            });
        return app;
    }
}